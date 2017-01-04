using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작물 정보가 저장된 DB입니다.
/// </summary>
/// 
[System.Serializable]
public class PlantDB  : MonoBehaviour{

    public static PlantDB instance;

    //ID
    [SerializeField]
    public int id = 0;
    //이름
    public string names = "";
    //설명
    public string content = "";
    //아이콘
    public Sprite icon;
    //인게임 이미지
    public Sprite inGamePlantSprites;
    //재배 시간
    public float growTime = 0;
    //식물 체력
    public float hp = 0, maxHP = 0;
    //얻는 아이템 수
    public int minValue = 0, maxValue = 0;
    //모에화 확률
    public float moePercent = 0;
    //색상 코드
    public Color32 colorCode;

    public Plant.BigKind bigkind;
    public Plant.SmallKind smallkind;
    public Plant.DetailKind detailkind;
    public Plant.Size size;
    public Plant.Taste taste;
    public Plant.ColorType colorType;
    public Plant.Durability durability;

    public List<TopPlant> topPlantList = new List<TopPlant>();
    public List<MiddlePlant> middlePlantList = new List<MiddlePlant>();
    public List<BottomPlant> bottomPlantList = new List<BottomPlant>();

    void Awake() {
        instance = this;
    }


    public void Add(int kind) {
        switch (kind) {
            case 0:
                topPlantList.Add(new TopPlant(id,name,content,icon,inGamePlantSprites,growTime,maxHP,minValue,maxValue,moePercent,colorCode,bigkind,smallkind,detailkind,size,taste,colorType,durability));
                break;
            case 1:
                middlePlantList.Add(new MiddlePlant(id, name, content, icon, inGamePlantSprites, growTime, maxHP, minValue, maxValue, moePercent, colorCode, bigkind, smallkind, detailkind, size, taste, colorType, durability));
                break;
            case 2:
                bottomPlantList.Add(new BottomPlant(id, name, content, icon, inGamePlantSprites, growTime, maxHP, minValue, maxValue, moePercent, colorCode, bigkind, smallkind, detailkind, size, taste, colorType, durability));
                break;
            default:
                Debug.LogError("존재하지 않는 요소");
                break;
        }
    }


}
