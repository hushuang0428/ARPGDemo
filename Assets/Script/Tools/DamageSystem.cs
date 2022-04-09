using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : Singleton<DamageSystem>
{
    public int GetDamage(BaseData exporter,BaseData undertaker,AttributeType type)
    {
        float damage = Mathf.Max(0, exporter.aggressivity - undertaker.defensive);
        
        damage *= UnityEngine.Random.value < exporter.AttributeDic[AttributeType.CriticalChance]? 1 + exporter.AttributeDic[AttributeType.CriticalMultiplier] : 1;

        damage *= (1 + exporter.AttributeDic[type] - undertaker.AttributeDic[type + 1]);

        return (int)damage;
    }
    
}
