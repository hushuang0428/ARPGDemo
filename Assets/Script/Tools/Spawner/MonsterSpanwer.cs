using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 当玩家到达怪物生成点附近时 生成怪物
/// </summary>




public class MonsterSpawner : BaseSpawner
{
    private MonsterData data;

    //与玩家等级匹配
    public MonsterSpawner(MonsterData data)
    {
        this.data = data;
        name = data.name.ToString();
        
    }

    //设置怪物的位置和等级
    public void UpDataSpanwer(int level,Transform home)
    {
        data.Level = level;
        data.home = home;
    }


    public override GameObject Spawner()
    {

        
        GameObject NewMonster = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(data.PrefabPath));

        BaseMonster script = NewMonster.GetComponent<BaseMonster>();
        script.data = this.data;

        BehaviorTree bt = NewMonster.GetComponent<BehaviorTree>(); bt.SetVariable("Home",(SharedTransform)data.home);



        return NewMonster; 
    }



    


}
