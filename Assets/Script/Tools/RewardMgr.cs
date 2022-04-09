using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ProbabilityTable
{
    public int Id;
    public float probability;
}

[Serializable]
public class  Reward
{
    //奖励总数
    public int quantity;
    //奖励列表
    [SerializeField]public List<int> rewards;
    //各自对应的概率
    [SerializeField] public Dictionary<int, float> probabilityTableDic = new Dictionary<int, float>();
    //Inspector辅助表
    [SerializeField] public ProbabilityTable[] probabilityTables;

    public void ToDic()
    {
        foreach(var arr in probabilityTables)
        {
            probabilityTableDic.Add(arr.Id, arr.probability);
        }
    }


}




/// <summary>
/// 配置不同怪物对应不同的奖励表
/// </summary>

[Serializable]
public class RewardMgr : SingletonMono<RewardMgr>
{

    //怪物对应奖励的配置表
    [SerializeField] public Dictionary<MonsterName, Reward> MonsterRewaedTable;

    //id对应物品的配置表
    [SerializeField]public Dictionary<int, BaseItem> IDToItemTable; 

    //概率逻辑还有改进空间
    public void GetReward(BaseMonster monster)
    {
        for(int i=0;i< MonsterRewaedTable[monster.data.name].quantity; i++)
        {
            foreach(var probabilityTable in MonsterRewaedTable[monster.data.name].probabilityTables)
            {
                if (UnityEngine.Random.value < probabilityTable.probability)
                {
                    GameObject rewaed=ObjectPool.Instance.GetObject(SpawnerMgr.Instance.itemSpawners[MonsterRewaedTable[monster.data.name].rewards[probabilityTable.Id]]);
                    rewaed.transform.position = monster.transform.position;
                }
            }

        }         
         
    }

    public void OnInit()
    {
        MonsterRewaedTable= JsonMgr.Instance.LoadJson<Serialization<MonsterName, Reward>>(ConstPath.CONFIG_REWARD_TABLE, "Reward.json").ToDictionary();

        MonsterRewaedTable[MonsterName.QQR].ToDic();
        
    }

}
