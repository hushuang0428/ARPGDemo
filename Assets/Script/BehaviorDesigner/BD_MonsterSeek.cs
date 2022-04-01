using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BD_MonsterSeek :Action
{

    public SharedGameObject character;
    public SharedTransform target;
    public SharedFloat arriveDistance;
    public SharedFloat speed;
    public SharedFloat angularSpeed;

    public override void OnStart()
    {
        //character.Value.GetComponent<Animator>().Play("Sprint");
    }
    public override TaskStatus OnUpdate()
    {
        if (target == null) return TaskStatus.Failure;

        transform.LookAt(target.Value.position);

        transform.position=Vector3.MoveTowards(transform.position, target.Value.position,Time.deltaTime*speed.Value);


        if (Vector3.Distance(transform.position, target.Value.position) <= arriveDistance.Value)
        {
            Debug.Log("Arrive");
            return TaskStatus.Success;
        }



        return TaskStatus.Running;
    }


}
