using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMgr : SingletonMono<AchievementMgr>
{
    [SerializeField] private List<Task> tasks = new List<Task>();
    //public List<SO_BaseCondition> leveConditions;
    protected override void Awake()
    {
        base.Awake();

    }
    private void Start()
    {
        //什么行为绑定什么条件还需要写个表
        //GameManager.Instance.player.UpLeve += this.UpLeve;
        FindObjectOfType<BaseCharacter>().UpLevel += this.UpLevel;
    }

    public void UpLevel()
    {

        if (tasks.Count != 0)
        {
            foreach (var task in tasks)
            {
                SO_BaseTask soTask = task.SOTask;

                if (soTask.ID > 10000&&soTask.finlish.ConditionSetUp())
                {
                    Debug.Log("完成"+soTask.ID+"号成就，获得"+soTask.reward.ID+"号物品");
                }
            }
        }
    }
}




