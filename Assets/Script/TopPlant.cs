using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TopPlant : Plant
{

    public TopPlant(int _id, string _names, string _content, Sprite _icon, Sprite _inGamePlantSprites, float _growTime, float _maxHP, int _minValue, int _maxValue, float _moePercent, Color32 _colorCode,Plant.BigKind _bigkind, Plant.SmallKind _smallkind, Plant.DetailKind _detailkind, Plant.Size _size, Plant.Taste _taste, Plant.ColorType _colorType, Plant.Durability _durability)
    {
        id = _id;
        names = _names;
        content = _content;
        icon = _icon;
        inGamePlantSprites = _inGamePlantSprites;
        growTime = _growTime;
        maxHP = _maxHP;
        minValue = _minValue;
        maxValue = _maxValue;
        moePercent = _moePercent;
        colorCode = _colorCode;
        bigkind = _bigkind;
        smallkind = _smallkind;
        detailkind = _detailkind;
        size = _size;
        taste = _taste;
        colorType = _colorType;
        durability = _durability;
    }

}
