using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class CharacterData
{

    
    [Header("基础人物信息")]
    public int ID;
    public CharactersName name;
    public WeaponType usageWeaponType;
    public string BackGroundStory;


    [Header("基础人物数据")]
    public string PrefabPath;



    [Header("数值数据")]
    public float speed;
    public float sprintSpeed;

    public int weaponId;

    public int Level;
    [SerializeField]private int currExp;
    [SerializeField]private int MaxExp=100;
    public float ExpMag;



    public int currHP;
    public int maxHP;
    public int aggressivity;



    
    public event Action UpLevel;
    

    [Header("固有属性")]
    [SerializeField] public List<AttributeData> inherentAttributes = new List<AttributeData>();

    [Header("成长属性")]
    [SerializeField] public List<AttributeData> growthAttributes = new List<AttributeData>();

    private Dictionary<AttributeType,float> AttributeDic = new Dictionary<AttributeType,float>();

    public void InitData()
    {

        
        foreach(var attribute in inherentAttributes)
        {
            
            UpdataData(attribute);
        }
        UpdataData(PlayerMainDataMgr.Instance.weaponBag[weaponId].mainAttribute);
        UpdataData(PlayerMainDataMgr.Instance.weaponBag[weaponId].secondaryAttribute);

    }

    //绑定到升级时自动运行
    public void upDateLevel()
    {
        

        foreach (var attribute in growthAttributes)
        {
            UpdataData(attribute);
        }
        currHP = maxHP;
        Level++;
        
        UpLevel();
    }

    public void UpdataData(AttributeData data)
    {
        //可以先将所有的初始化 然后再switch
        if (!AttributeDic.ContainsKey(data.type))
        {
            AttributeDic.Add(data.type, data.vlaue);
        }
        else
            AttributeDic[data.type] += data.vlaue;

       
        if(AttributeDic.ContainsKey(AttributeType.HP)&& AttributeDic.ContainsKey(AttributeType.HPP))
            maxHP = (int)(AttributeDic[AttributeType.HP] * (1 + AttributeDic[AttributeType.HPP]));
        
        if (AttributeDic.ContainsKey(AttributeType.Aggressivity) && AttributeDic.ContainsKey(AttributeType.AggressivityP))
            aggressivity = (int)(AttributeDic[AttributeType.Aggressivity] * (1 + AttributeDic[AttributeType.AggressivityP]));
 
        
        /*
        switch (data.type)
        {
           
            case AttributeType.HP: 
            case AttributeType.HPP:
                maxHP = (int)(AttributeDic[AttributeType.HP] * (1 + AttributeDic[AttributeType.HPP]));break;
            case AttributeType.Aggressivity:
            case AttributeType.AggressivityP:
                aggressivity= (int)(AttributeDic[AttributeType.Aggressivity] * (1 + AttributeDic[AttributeType.AggressivityP])); break;
        }
        */
    }


    public void GetExp(int exp)
    {
        currExp += exp;
        if (currExp >= MaxExp) 
        { 
            currExp -= MaxExp; 
            MaxExp = (int)(MaxExp*ExpMag);
            upDateLevel();
        }
    }






}
