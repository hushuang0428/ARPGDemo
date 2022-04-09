using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public struct MonsterRewardTable
{
    public MonsterName name;
    public Reward reward;
}






public class ATest :BaseTest
{
    private float xVelocity;
    private float yVelocity;

    public Dictionary<int, float> test;
    public Serialization<int, float> Test;

    public MonsterRewardTable[] monsterRewardTable;

    public GameObject obj;

    /// <summary>
    /// 写成一个加载类
    /// </summary>
    public virtual void Start()
    {
        base.Start();
        /*
        Dictionary<MonsterName, Reward> tt = new Dictionary<MonsterName, Reward>();

        tt=JsonMgr.Instance.LoadJson<Serialization<MonsterName, Reward>>("Resources", "Test.json").ToDictionary();

        Debug.Log(tt[MonsterName.QQR].quantity);

        //monsterRewardTable[0] = tt[MonsterName.QQR];
        tt.Add(MonsterName.QQR, reward);
        foreach(var arr in monsterRewardTable)
        {
            tt.Add(arr.name, arr.reward);
        }*/

        //JsonMgr.Instance.SaveByJson<Serialization < MonsterName, Reward >> ("Resources","Test.json", new Serialization<MonsterName, Reward>(tt));
        

        //reward = tt[MonsterName.QQR];
        //tt[MonsterName.QQR].ToDic();
        //Debug.Log(tt[MonsterName.QQR].probabilityTable[1]);

        //RewardMgr.Instance.OnInit();


    }
    private void Update()
    {
        xVelocity = Input.GetAxis("Horizontal");
        yVelocity = Input.GetAxis("Vertical");

        transform.Translate(xVelocity*0.1f, 0f, yVelocity*0.1f);
    }

}


public class A{
    public int a = 10;
}
