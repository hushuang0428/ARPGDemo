using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BD_MonsterIdle : Action
{
    public SharedGameObject monster;
   

    public override void OnAwake()
    {
       
        

    }

    public override void OnStart()
    {
        monster.Value.GetComponent<Animator>().Play("Idle");
        

    }



    public override TaskStatus OnUpdate()
    {
        //
        return TaskStatus.Running;
    }
}
