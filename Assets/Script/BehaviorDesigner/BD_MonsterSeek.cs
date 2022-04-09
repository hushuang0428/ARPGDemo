using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using TooltipAttribute = UnityEngine.TooltipAttribute;

public class BD_MonsterSeek :Action
{

    [Tooltip("The speed of the agent")]
    public SharedFloat speed;
    [Tooltip("Angular speed of the agent")]
    public SharedFloat angularSpeed;
    [Tooltip("The agent has arrived when the magnitude is less than this value")]
    public SharedFloat arriveDistance = 0.1f;
    [Tooltip("The transform that the agent is moving towards")]
    public SharedTransform targetTransform;
    [Tooltip("If target is null then use the target position")]
    public SharedVector3 targetPosition;

    private Animator anim;

    // True if the target is a transform
    private bool dynamicTarget;
    // A cache of the NavMeshAgent
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    public override void OnAwake()
    {
        // cache for quick lookup
        navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        
        
    }

    public override void OnStart()
    {
        
        anim.Play("Sprint");
        
        // the target is dynamic if the target transform is not null and has a valid
        dynamicTarget = (targetTransform != null && targetTransform.Value != null);

        // set the speed, angular speed, and destination then enable the agent
        navMeshAgent.speed = speed.Value;
        navMeshAgent.angularSpeed = angularSpeed.Value;
        navMeshAgent.enabled = true;
        navMeshAgent.destination = Target();
    }

    // Seek the destination. Return success once the agent has reached the destination.
    // Return running if the agent hasn't reached the destination yet
    public override TaskStatus OnUpdate()
    {

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < arriveDistance.Value)
        {
            return TaskStatus.Success;
        }

        // Update the destination if the target is a transform because that agent could move
        if (dynamicTarget)
        {
            navMeshAgent.destination = Target();
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // Disable the nav mesh
        navMeshAgent.enabled = false;
        anim.Play("Idle");
    }

    // Return targetPosition if targetTransform is null
    private Vector3 Target()
    {
        if (dynamicTarget)
        {
            return targetTransform.Value.position;
        }
        return targetPosition.Value;
    }

    // Reset the public variables
    public override void OnReset()
    {
        arriveDistance = 0.1f;
    }
}


