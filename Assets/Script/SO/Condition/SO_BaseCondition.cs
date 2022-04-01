using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SO_BaseCondition : ScriptableObject
{

    public int ID;

    public abstract bool ConditionSetUp();
   
}
