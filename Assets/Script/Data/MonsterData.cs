using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MonsterName
{
    QQR
}

[Serializable]
public class MonsterData :BaseData
{
    [Header("基础怪物信息")]
    public int ID;
    public MonsterName name;
    public string BackGroundStory;


    [Header("基础怪物数据")]
    public string PrefabPath;

    public Transform home;



    [Header("数值数据")]
    public float speed;
    public float sprintSpeed;


    
    public int Level;


    //[SerializeField]List<AttributeData> attributes = new List<AttributeData>();

    //private Dictionary<AttributeType, float> attributeDic = new Dictionary<AttributeType, float>();



    public override void InitData()
    {
        base.InitData();
        currHP = maxHP;
    }
    

    /*public void UpdataData(AttributeData data)
    {
        //可以先将所有的初始化 然后再switch
        attributeDic[data.type] += data.vlaue;

        switch (data.type)
        {
            case AttributeType.HP:
            case AttributeType.HPP:
                maxHP = (int)(attributeDic[AttributeType.HP] * (1 + attributeDic[AttributeType.HPP])); break;
            case AttributeType.Aggressivity:
            case AttributeType.AggressivityP:
                aggressivity = (int)(attributeDic[AttributeType.Aggressivity] * (1 + attributeDic[AttributeType.AggressivityP])); break;
        }

    }
    */


}
