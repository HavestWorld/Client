using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인게임을 관리하는 게임 매니저입니다.
/// 시간의 업데이트를 비롯한 모든 것을 처리하고 있습니다.
/// </summary>
public class GameManager : MonoBehaviour
{
    //싱글 톤
    public static GameManager instance;

    public float playTime = 0, timeScale = 0;
    public int day = 1, nightCnt = 0, hour = 0, min = 0, sec = 0, ABS = 1;
    public float plusTime = 2;
    public bool night = true;

    public Sprite[] daySprites;
    public Color32[] dayColors;

    public Text timeTxt, dateTxt;
    public Image dayIcon, weatherIcon, backgroundDate;
    public Slider timeLine;

    public GameObject Spins;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        DayGradientManager.instance.SetNight(0);
        dayIcon.sprite = daySprites[nightCnt];
        dayIcon.color = dayColors[nightCnt];
    }

    public void Update()
    {
        TimeUpdate();
        if (Input.GetMouseButtonUp(0))
            playTime += plusTime * ABS;
    }

    /// <summary>
    /// 해와 달 아이콘을 변경합니다.
    /// </summary>
    public void DaySet()
    {
        dayIcon.sprite = daySprites[nightCnt];
        dayIcon.color = dayColors[nightCnt];
    }

    /// <summary>
    /// 시간을 업데이트하는 함수
    /// </summary>
    public void TimeUpdate()
    {

        //시간을 추가
        playTime += Time.deltaTime * ABS;
        
        if ((playTime - 43200 > 0 && ABS > 0) || (playTime < 0 && ABS < 0))
        {
            ABS *= -1;
            playTime = 43200;
            ++nightCnt;
            if (nightCnt == 2)
            {
                DayUpdate();
            }
            else {
                DayGradientManager.instance.StartCoroutine("OffsetAnimation", 1);
            }
            dayIcon.sprite = daySprites[nightCnt];
            dayIcon.color = dayColors[nightCnt];
        }
        hour = (int)((min / 60) % 12);
        min = (int)((playTime / 60));

        timeTxt.text = string.Format("{0:00} : {1:00}", hour, Mathf.Abs(min - (hour * 60)));

        timeScale = playTime / 43200 * 100;
        timeLine.value = timeScale / 100;
    }

    /// <summary>
    /// 날짜를 업데이트하는 함수
    /// </summary>
    public void DayUpdate()
    {
        ++day;
        playTime = 0;
        nightCnt = 0;
        DayGradientManager.instance.StartCoroutine("OffsetAnimation",3);
        dateTxt.text = string.Format("Jun. {0:00}", day);
    }

}
