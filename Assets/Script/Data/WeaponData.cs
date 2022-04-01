using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum WeaponName
{
   狼末
}
public enum WeaponType
{
    Epee
}

[Serializable]
public class WeaponData 
{
    [Header("基础武器信息")]
    public int ID;
    public WeaponName name;
    public WeaponType type;
    public string BackGroundStory;

    public bool isused;


    [Header("基础武器数据")]
    public string PrefabPath;


    public AttributeData mainAttribute;
    public AttributeData secondaryAttribute;

   //TODO:武器效果动态绑定到函数
}

[Serializable]
public class Weapon: BaseItem
{
    [SerializeField]public WeaponData data;
    public Weapon()
    {
        type = ItemType.Weapon;
        
    }
    
}

