using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메인씬의 배경 그라데이션을 관리하는 클래스입니다.
/// </summary>
[ExecuteInEditMode]
class DayGradientManager : MonoBehaviour
{
   
    public static DayGradientManager instance;

    [Range(0.001f, 0.999f)]
    public float offset = 0.999f;

    public float marign = 0.1f;

    public float timeSlice = 0.01f;

    public Material[] materials;

    public MeshRenderer[] meshRender;

    Animator ani;

    public void Awake()
    {
        instance = this;
        ani = GetComponent<Animator>();
    }

    /// <summary>
    /// 배경 애니메이션을 실행하는 함수
    /// 0 = 낮 
    /// 1 = 낮 > 밤 <자동처리중>
    /// 2 = 밤
    /// 3 = 밤 > 낮 <자동처리중>
    /// </summary>
    /// <param name="session"> 낮 = 0  / 밤 = 3</param>
    /// <returns></returns>
    public IEnumerator OffsetAnimation(int session)
    {
        meshRender[0].material = materials[session];
        meshRender[1].material = materials[(session+1)%4];

        while (offset > 0)
        {
            //첫번째 배경에 애니메이션이 시작됩니다.
            meshRender[0].material.SetFloat("_Middle", offset);
            offset -= marign;
            //절반이 넘어가면 움직이기를 시작합니다.
            yield return new WaitForSeconds(timeSlice);
        }
        ani.SetBool("On", true);
        //오프셋을 초기화 합니다.
        offset = 0.999f;

        while (offset > 0)
        {
            //두번째 배경에 애니메이션이 시작됩니다.
            meshRender[1].material.SetFloat("_Middle", offset);
            offset -= marign;
            yield return new WaitForSeconds(timeSlice);
        }
        offset = 0.999f;
        if(session == 1)
            SetNight(1);
        else if(session == 3)
            SetNight(0);
        ani.SetBool("On", false);
    }

    /// <summary>
    /// 강제적인 낮과 밤을 세팅합니다. 
    /// 0 = 낮  / 1 = 밤
    /// </summary>
    /// <param name="night"> 낮 = 0 / 밤 = 1 </param>
    public void SetNight(int night) {
        switch (night) {
            case 0:
                meshRender[0].material = materials[0];
                meshRender[0].material.SetFloat("_Middle", 0.001f);
                break;
            case 1:
                meshRender[0].material = materials[2];
                meshRender[0].material.SetFloat("_Middle", 0.001f);
                break;
            default:
                Debug.LogError("존재하지 않는 값");
                break;
        }

    }

}

