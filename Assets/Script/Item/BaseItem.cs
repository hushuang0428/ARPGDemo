using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum ItemType
{
    Weapon
}











[Serializable]
public class BaseItem:MonoBehaviour
{
    [SerializeField]public ItemType type;
    [SerializeField]public int ID;
    
}
