using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="LeveCondition",menuName ="Task/Condition/LeveCondition")]
public class LeveConition : SO_BaseCondition
{
    public int limitLevel;

    public override bool ConditionSetUp()
    {
        return GameManager.Instance.player.data.Level >= limitLevel;
    }
}
