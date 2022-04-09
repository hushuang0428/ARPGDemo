using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum AttributeType
{
    HP,HPP,Aggressivity,AggressivityP,Defensive,DefensiveP, CriticalChance, CriticalMultiplier, PhysicalA, PhysicalR
}

[Serializable]
public class AttributeData 
{

    public AttributeType type;
    public float vlaue;


}
