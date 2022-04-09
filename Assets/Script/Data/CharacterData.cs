using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class CharacterData:BaseData
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

    public event Action UpLevel;
    
    
    [Header("成长属性")]
    [SerializeField] public List<AttributeData> growthAttributes = new List<AttributeData>();

    private Dictionary<AttributeType,float> AttributeDic = new Dictionary<AttributeType,float>();

    public override void InitData()
    {
        base.InitData();
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
