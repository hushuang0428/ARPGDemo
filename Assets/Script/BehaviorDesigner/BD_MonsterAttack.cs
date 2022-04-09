using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BD_MonsterAttack : Action
{

    private Animator anim;
    private float timer = 1f;


    public override void OnAwake()
    {

        anim = GetComponent<Animator>();

    }

    public override void OnStart()
    {
        
        anim.Play("Attack");
    }



    public override TaskStatus OnUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 1f;
            return TaskStatus.Success;
        } 
            


        return TaskStatus.Running;
    }


    public override void OnEnd()
    {
        base.OnEnd();
        anim.Play("Idle");
    }
}
