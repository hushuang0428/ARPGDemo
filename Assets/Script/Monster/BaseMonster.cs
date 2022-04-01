using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    public Rigidbody rigidbody;
    public BoxCollider coll;

    public Animator anim;

    //public SO_BaseCharacterData baseData;

    public DamageCalcuation damageCalc;

    public FSM fsm;

    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        
        //baseData = Resources.Load<SO_BaseCharacterData>("Config/Monster/MonsterData");
        
        //damageCalc = new DamageCalcuation(baseData);

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
}
