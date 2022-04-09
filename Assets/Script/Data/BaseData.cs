using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseData 
{
    public int currHP;
    public int maxHP;
    public int aggressivity;
    public int defensive;


    [Header("固有属性")]
    [SerializeField] public List<AttributeData> inherentAttributes = new List<AttributeData>();


    public readonly Dictionary<AttributeType, float> AttributeDic = new Dictionary<AttributeType, float>();

    

    public virtual void InitData()
    {
        foreach (AttributeType name in Enum.GetValues(typeof(AttributeType)))
        {
            AttributeDic.Add(name, 0f);
        }

        foreach (var attribute in inherentAttributes)
        {

            UpdataData(attribute);
        }
    }


    public void UpdataData(AttributeData data)
    {
        //可以先将所有的初始化 然后再switch
        AttributeDic[data.type] += data.vlaue;

        switch (data.type)
        {
            case AttributeType.HP:
            case AttributeType.HPP:
                maxHP = (int)(AttributeDic[AttributeType.HP] * (1 + AttributeDic[AttributeType.HPP])); break;
            case AttributeType.Aggressivity:
            case AttributeType.AggressivityP:
                aggressivity = (int)(AttributeDic[AttributeType.Aggressivity] * (1 + AttributeDic[AttributeType.AggressivityP])); break;
            case AttributeType.Defensive:
            case AttributeType.DefensiveP:
                defensive = (int)(AttributeDic[AttributeType.Defensive] * (1 + AttributeDic[AttributeType.DefensiveP]));break;
            default:break;
        }

    }


    public void GetDamage(int damage)
    {
        //currHP = currHP - damage < 0 ? 0 : currHP - currHP;
        currHP -= currHP < damage ? currHP : damage;
    }



}
