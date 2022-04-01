using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum AttributeType
{
    HP,HPP,Aggressivity,AggressivityP
}

[Serializable]
public class AttributeData 
{

    public AttributeType type;
    public float vlaue;


}
