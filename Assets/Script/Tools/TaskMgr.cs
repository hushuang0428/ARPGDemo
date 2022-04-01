using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TaskState
{
    ToBeTrigger,ToBeReceived,Running,FinlishedButNoReward,ReceiveReward
}

public class TaskMgr : SingletonMono<TaskMgr>
{
    [SerializeField] private List<Task> tasks = new List<Task>();

    public List<Task> triggeredList = new List<Task>();
    public List<Task> finlishList = new List<Task>();

    public static Task currTask;

    private void Awake()
    {
        base.Awake();
        
    }

    public void Start()
    {
        GameManager.Instance.player.data.UpLevel += this.TaskCondition;
    }
    
    private void TaskCondition()
    {
        if (currTask != null)
        {
            if (currTask.SOTask.finlish.ConditionSetUp())
            {
                Debug.Log(currTask.SOTask.ID + "已完成！！！");
                finlishList.Add(currTask);
                currTask.state = TaskState.FinlishedButNoReward;
                currTask = null;
            }
        }

        foreach(var task in tasks)
        {
            if (task.SOTask.trigger.ConditionSetUp())
            {
                if (!triggeredList.Contains(task))
                {
                    triggeredList.Add(task);
                    Debug.Log(task.SOTask.ID + "已解锁！！！");

                    task.state = TaskState.ToBeReceived;

                    //判断是否为日常任务
                    if(task.SOTask.ID>10000)
                    tasks.Remove(task);
                }
                
            }
        }
            

    }

    public void ReceiveTask(int taskID)
    {
        if (currTask != null)
        {

            Debug.Log("请先完成当前任务！！！");

            return;
        }
        foreach(var task in triggeredList)
        {
            if (task.SOTask.ID == taskID)
            {
                currTask = task;
                currTask.state = TaskState.Running;
                Debug.Log("已接取"+currTask.SOTask.ID);
                triggeredList.Remove(task);
                return;
            }
        }
    }

    public SO_BaseReward Reward(int taskID)
    {
        
        foreach(var task in finlishList)
        {
            if (taskID == task.SOTask.ID)
            {
                SO_BaseReward reward = task.SOTask.reward;
                finlishList.Remove(task);
                Debug.Log("已领取" + task.SOTask.ID + "任务奖励");
                return reward;
            }
        }
        return null;
        
    }

}


[Serializable]
public class Task
{
    public SO_BaseTask SOTask;
    public TaskState state; 

    public Task(SO_BaseTask task)
    {
        this.SOTask = task;
    }

}
