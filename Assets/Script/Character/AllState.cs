using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class NotInMissingState : IState
{
    public BaseCharacter character;

    public NotInMissingState(BaseCharacter character)
    {
        this.character = character;
    }

    public override void OnEnter()
    {
        
    }


    public override void OnExit()
    {
        character.myFsm.Pop();
    }

    public override void OnUpDate()
    {
        if (Input.GetMouseButtonDown(1))
            character.myFsm.TransitionState(StateType.Miss);
    }

}

public class NotInAttackState : NotInMissingState
{
    public NotInAttackState(BaseCharacter character) : base(character)
    {

    }

    public override void OnUpDate()
    {
        base.OnUpDate();
        if (Input.GetMouseButtonDown(0))
            character.myFsm.TransitionState(StateType.Attack);
    }
   
}

public class InMoveState : NotInAttackState
{
    public InMoveState(BaseCharacter character) : base(character) { }

    public override void OnUpDate()
    {
        base.OnUpDate();

        if (character.xVelocity == 0 && character.yVelocity == 0)
            base.OnExit();
    }
   
}

public class NotInMoveState : NotInAttackState
{
    public NotInMoveState(BaseCharacter character) : base(character)
    {

    }

    public override void OnUpDate()
    {
        base.OnUpDate();

        if (character.xVelocity != 0 || character.yVelocity != 0)
            character.myFsm.TransitionState(StateType.Move);
    }
    public override void OnExit()
    {
        
    }
}

    /// <summary>
    /// 非idle状态不可以切换角色
    /// </summary>

public class IdleState : NotInMoveState
{

        

    public IdleState(BaseCharacter character) : base(character)
    {
            
    }

    public override void OnEnter()
    {
        character.anim.Play("Idle");
    }

    public override void OnUpDate()
    {
        base.OnUpDate();
            
    }

    public override void OnExit()
    {

    }
}

public class MoveState : InMoveState
{

    public MoveState(BaseCharacter character):base(character)
    {
        
    }

    public override void OnEnter()
    {
        character.anim.Play("Move");
    }

    public override void OnUpDate()
    {
        base.OnUpDate();
        character.transform.Translate(character.xVelocity * character.speed, 0f, character.yVelocity * character.speed);

        if (character.IsSprint&& character.yVelocity > 0&& character.xVelocity == 0)
            character.myFsm.TransitionState(StateType.Sprint);

    }
   
    public override void OnExit()
    {
        base.OnExit();
    }
}

public class SprintState : InMoveState
{

    public SprintState(BaseCharacter character):base(character)
    {
       
       

    }


    public override void OnEnter()
    {
        character.anim.Play("Sprint");
        
    }

    public override void OnUpDate()
    {
        base.OnUpDate();
        character.transform.Translate(character.xVelocity * character.sprintSpeed, 0f, character.yVelocity * character.sprintSpeed);

    }


    public override void OnExit()
    {
        base.OnExit();
    }
}


public class AttackState : NotInMissingState
{

    public float CombatTimer;

    public AttackState(BaseCharacter character) : base(character)
    {

    }

    public override void OnEnter()
    {
        character.IsAttack = true;
        character.rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        character.anim.Play("Attack"); 
        CombatTimer = 0.8f;
        

    }

    public override void OnUpDate()
    {
        base.OnUpDate();

        character.transform.Translate(0f,0f,character.speed*0.1f);
        CombatTimer -= Time.deltaTime;

        if (CombatTimer<0&& character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            OnExit();
        }
        
            
        else if (Input.GetMouseButtonDown(0))
        {
            CombatTimer = 0.8f;
           
        }
       
            
    }

    public override void OnExit()
    {
       
        character.IsAttack = false;
        character.rigidbody.constraints = RigidbodyConstraints.None;
        character.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        base.OnExit();
    }
}

public class MissState : IState
{

    BaseCharacter character;
    

    public MissState(BaseCharacter character)
    {
        this.character = character;
    }


    public override void OnEnter()
    {
        character.anim.Play("Miss");

        //无敌帧判断用
        character.IsMiss = true;
    } 

    public override void OnUpDate()
    {
        character.transform.Translate(0f, 0f,  1f* character.speed);

        //使用下推机
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            OnExit();
            //character.myFsm.TransitionState(StateType.Idle);


    }

    public override void OnExit()
    {
        character.IsMiss = false;
        character.myFsm.Pop();
    }
}

public class GetHitState : IState
{

    BaseCharacter character;

    public GetHitState(BaseCharacter character)
    {
        this.character = character;
    }


    public override void OnEnter()
    {
        


    }

    public override void OnUpDate()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            OnExit();
    }

    public override void OnExit()
    {
        character.myFsm.Pop();
    }
}


/// <summary>
/// 怪物类状态
/// </summary>

public class MIdleState : IState
{

    BaseMonster monster;

    public MIdleState(BaseMonster monster)
    {
        this.monster = monster;
    }


    public override void OnEnter()
    {

        
        monster.anim.Play("Idle");

    }

    public override void OnUpDate()
    {
        
    }

    public override void OnExit()
    {

    }
}


public class MDeadState : IState
{

    

    BaseMonster monster;

    public MDeadState(BaseMonster monster)
    {
        this.monster = monster;
    }


    public override void OnEnter()
    {
        monster.bt.enabled = false;

        
        Debug.Log("Monster dead");

        monster.anim.Play("Death");

        
    }

    public override void OnUpDate()
    {
        if(monster.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
        {
            
            RewardMgr.Instance.GetReward(monster);

            ObjectPool.Instance.PushObject(monster.gameObject);
        }
    }

    public override void OnExit()
    {

    }
}

public class MGetHitState : IState
{

    public BaseMonster monster;

    public MGetHitState(BaseMonster monster)
    {
        this.monster = monster;
    }


    public override void OnEnter()
    {
        monster.anim.StopPlayback();
        monster.anim.Play("GetHit");

    }

    public override void OnUpDate()
    {
        if (monster.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
            monster.fsm.TransitionState(StateType.MIdle);
    }

    public override void OnExit()
    {

    }
}
