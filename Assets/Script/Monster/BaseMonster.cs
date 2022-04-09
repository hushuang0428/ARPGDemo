using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    public Rigidbody rigidbody;
    public BoxCollider coll;

    public Animator anim;

    public BehaviorTree bt;

    //public SO_BaseCharacterData baseData;

    public DamageCalcuation damageCalc;

    public FSM fsm;

    public MonsterData data;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        bt = GetComponent<BehaviorTree>();
    }


    void OnEnable()
    {
        data.InitData();
    }

    private void Start()
    {

        fsm = new FSM();
        fsm.AddState(StateType.MIdle, new MIdleState(this));
        fsm.AddState(StateType.MDead, new MDeadState(this));
        fsm.AddState(StateType.MGetHit, new MGetHitState(this));
        fsm.TransitionState(StateType.MIdle);

    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnTrick();
    }

    public void GetDamage(int damge)
    {
        data.GetDamage(damge);
        if (data.currHP == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        fsm.TransitionState(StateType.MDead);
    } 









}
