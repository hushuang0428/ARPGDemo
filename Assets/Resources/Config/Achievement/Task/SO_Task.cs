using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Task/NewTask",fileName ="NewTask")]
public class SO_BaseTask : ScriptableObject
{
    public int ID;
    public string TaksDescribe;
    public SO_BaseCondition trigger;
    public SO_BaseCondition finlish;
    public SO_BaseReward reward;
}
