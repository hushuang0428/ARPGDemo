using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





/// <summary>
/// 非idle状态不可以切换角色
/// </summary>

public class IdleState : IState
{

    BaseCharacter character;

    public IdleState(BaseCharacter character)
    {
        this.character = character;
    }


    public void OnEnter()
    {
        character.anim.Play("Idle");
    }

    public void OnUpDate()
    {
        if (Input.GetMouseButtonDown(1))
            character.myFsm.TransitionState(StateType.Miss);

        if (character.xVelocity != 0 || character.yVelocity != 0)
            character.myFsm.TransitionState(StateType.Move);
        if (Input.GetMouseButtonDown(0))
           character.myFsm.TransitionState(StateType.Attack);
    }

    public void OnExit()
    {

    }
}



public class MoveState : IState
{

    BaseCharacter character;
    

    public MoveState(BaseCharacter character)
    {
        this.character = character;
    }


    public void OnEnter()
    {
        character.anim.Play("Move");
    }

    public void OnUpDate()
    {
        if (Input.GetMouseButtonDown(1))
            character.myFsm.TransitionState(StateType.Miss);

        character.transform.Translate(character.xVelocity * character.speed, 0f, character.yVelocity * character.speed);

        if (character.xVelocity==0&& character.yVelocity==0) 
            character.myFsm.TransitionState(StateType.Idle);

        if (character.IsSprint&& character.yVelocity > 0&& character.xVelocity == 0)
            character.myFsm.TransitionState(StateType.Sprint);

        if (Input.GetMouseButtonDown(0))
            character.myFsm.TransitionState(StateType.Attack);


    }
   

    public void OnExit()
    {

    }
}


public class SprintState : IState
{

    BaseCharacter character;
    

    public SprintState(BaseCharacter character)
    {
        this.character = character;
       

    }


    public void OnEnter()
    {
        character.anim.Play("Sprint");
        
    }

    public void OnUpDate()
    {
        if (Input.GetMouseButtonDown(1))
            character.myFsm.TransitionState(StateType.Miss);

        character.transform.Translate(character.xVelocity * character.sprintSpeed, 0f, character.yVelocity * character.sprintSpeed);
        if (!character.IsSprint)
            character.myFsm.TransitionState(StateType.Move);
        if (character.xVelocity == 0 && character.yVelocity == 0)
            character.myFsm.TransitionState(StateType.Idle);
        if (Input.GetMouseButtonDown(0))
            character.myFsm.TransitionState(StateType.Attack);


    }


    public void OnExit()
    {
       
    }
}


public class AttackState : IState
{

    BaseCharacter character;
   

    public float CombatTimer;
    
    

    public AttackState(BaseCharacter character)
    {
        this.character = character;
        
        
    }


    public void OnEnter()
    {
        character.IsAttack = true;
       
        character.anim.Play("Attack"); 
        CombatTimer = 0.8f;
        

    }

    public void OnUpDate()
    {
        if (Input.GetMouseButtonDown(1))
            character.myFsm.TransitionState(StateType.Miss);

        character.transform.Translate(0f,0f,character.speed*0.1f);
        CombatTimer -= Time.deltaTime;

        if (CombatTimer<0&& character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
        {
            character.myFsm.TransitionState(StateType.Idle);
        }
        
            
        else if (Input.GetMouseButtonDown(0))
        {
            CombatTimer = 0.8f;
           
        }
       
            
    }

    public void OnExit()
    {
        character.IsAttack = false;
    }
}

public class MissState : IState
{

    BaseCharacter character;
    

    public MissState(BaseCharacter character)
    {
        this.character = character;
        

        
        
    }


    public void OnEnter()
    {
        character.anim.Play("Miss");

        //无敌帧判断用
        character.IsMiss = true;
    } 

    public void OnUpDate()
    {
        character.transform.Translate(0f, 0f,  1f* character.speed);

        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            character.myFsm.TransitionState(StateType.Idle);


    }

    public void OnExit()
    {
        character.IsMiss = false;
    }
}

public class GetHitState : IState
{

    BaseCharacter character;

    public GetHitState(BaseCharacter character)
    {
        this.character = character;
    }


    public void OnEnter()
    {
        


    }

    public void OnUpDate()
    {
        if (character.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            character.myFsm.TransitionState(StateType.Idle);
    }

    public void OnExit()
    {

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


    public void OnEnter()
    {

        
        monster.anim.Play("Idle");

    }

    public void OnUpDate()
    {
        /*
        if (monster.baseData.currHP <= 0)
        {
            
            monster.fsm.TransitionState(StateType.MDead);
        }*/
    }

    public void OnExit()
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


    public void OnEnter()
    {
        //monster.baseData.currHP = 0;
        Debug.Log("Monster dead");
        GameObject.Destroy(monster.gameObject);
    }

    public void OnUpDate()
    {
        
    }

    public void OnExit()
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


    public void OnEnter()
    {

        monster.anim.Play("GetHit");

    }

    public void OnUpDate()
    {
        if (monster.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
            monster.fsm.TransitionState(StateType.MIdle);
    }

    public void OnExit()
    {

    }
}