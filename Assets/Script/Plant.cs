using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작물의 최상위 부모 클래스입니다.
/// </summary>
/// 
[System.Serializable]
public class Plant
{
    //ID
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

    public BigKind bigkind;
    public SmallKind smallkind;
    public DetailKind detailkind;
    public Size size;
    public Taste taste;
    public ColorType colorType;
    public Durability durability;

    /// <summary>
    /// 일반, 특수 작물의 분류
    /// </summary>
    //대분류 < 일반 , 특수 >
    public enum BigKind
    {
        //일반
        normal,
        //특수
        special
    }

    /// <summary>
    /// 식용, 비식용 작물의 분류
    /// </summary>
    //소분류 < 식용 가능성 >
    public enum SmallKind
    {
        //식용
        edible,
        //비식용
        nonEdible
    }

    /// <summary>
    /// 작물의 특성의 분류
    /// </summary>
    //세부 분류 < 작물의 특성 >
    public enum DetailKind
    {
        //곡류
        cereal,
        //콩류
        pulse,
        //구근류
        bulbous,
        //채소
        vegetable,
        //과실류
        fruit,
        //종실류
        seedling,
        //특용작물
        specialCrops,
        //약용작물
        medicinalCrop,
        //전매작물
        resoldCrop,
        //화훼작물
        flowerCrop,
        //버섯류
        mushroom,
        //관상식물
        ornamentalPlants,
        //원예식물
        gardeningPlant,
        //공업용 작물
        industrialCrop,
        //육류
        meat,
        //우유
        milk,
        //수산물
        AquaticProducts,
        //돈
        money,
        //불
        fire,
        //도우미
        helper,
        //고무
        Liquid,
        //기름
        Rubber
    }

    /// <summary>
    /// 작물의 사이즈 분류
    /// </summary>
    public enum Size
    {
        //거대한
        giant,
        //큰
        big,
        //(일반적인) - 적당한
        normal,
        //작은
        small,
        //개미만한                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        little
    }

    /// <summary>
    /// 작물의 맛의 분류
    /// </summary>
    public enum Taste
    {
        //단맛
        sweet,
        //짠맛
        solty,
        //신맛
        sour,
        //쓴맛
        bitter,
        //감칠맛
        savory,
        //떫은맛
        turb,
        //매운맛
        spicy
    }

    /// <summary>
    /// 작물의 색상의 분류
    /// </summary>
    public enum ColorType
    {
        //빨강색
        red,
        //주황색
        orange,
        //노랑색
        yellow,
        //초록색
        green,
        //파랑색
        blue,
        //보라색
        purple,
        //분홍색
        pink,
        //연두색
        rightGreen,
        //하늘색
        skyBlue,
        //은색
        silber,
        //금색
        gold,
        //갈색
        brown,
        //흰색
        white,
        //다홍색
        scarlet,
        //회색
        gray,
        //연보라색
        mellow,
        //검정색
        black,
        //살구색
        fleshy
    }

    /// <summary>
    /// 작물의 내구성의 분류
    /// </summary>
    public enum Durability
    {
        //단단한
        hard,
        //튼튼한
        strong,
        //적당한
        normal,
        //약한
        week,
        //연약한
        weekness
    }

}
