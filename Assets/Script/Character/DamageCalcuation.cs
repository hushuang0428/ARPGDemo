using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum DamageType
{
    Physical
}
public struct BuffInfo
{
    public float buff, deBuff;
    public BuffInfo(float buff,float deBuff) { this.buff = buff;this.deBuff = deBuff; }
    
}

public class DamageCalcuation
{
    public CharacterData baseData;
    
    public DamageCalcuation(CharacterData baseData)
    {
        this.baseData = baseData;

        //baseData.InitDBI();


        
    }

    public void UpDataDBI(Attribute attribute,float timer) 
    {
        //TODO:持续更新buff，应用协程
        //乘除法公式来规划伤害     

    }

    public void UpDataDBI(Attribute attribute)
    {
        //TODO:获取固有buff

    }
/*
    public int OutputDamage(DamageType damageType,CharacterData receptor)
    {
        
        if (receptor == null) return 0;
        //基础计算
        float basedamage = baseData.aggressivity * (1 + baseData.damageBuffInfo[damageType].buff);
        //暴击计算
        basedamage *= UnityEngine.Random.value < baseData.criticalChance ? 1+baseData.criticalMultiplier : 1;

        
        return (int)(basedamage * Mathf.Min(1,1 - Mathf.Min(receptor.damageBuffInfo[damageType].deBuff,1)));
       
    }
 */

    public void UpDataHP(int value)
    {
        //baseData.currHP = Mathf.Min(baseData.maxHp, baseData.currHP + value);
    }

   
     

}
