using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum StateType 
{
    Idle,Move,Attack,Sprint,Miss,GetHit,
    MIdle,MDead, MGetHit
}

public class FSM 
{

    private Stack<IState> stateSrack = new Stack<IState>();

    //private IState currentstate;

    

    Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();


    public void AddState(StateType type,IState state)
    {
        if (states.ContainsKey(type)) return;
        states.Add(type, state);
    }
    
    public void OnTrick()
    {
        stateSrack.Peek()?.OnUpDate();
    }
    
    public void TransitionState(StateType type)
    {
        /*
        if (currentstate != null)
            currentstate?.OnExit();
        currentstate = states[type];
        currentstate?.OnEnter();
        */
       
        if (stateSrack.Count > 0)
        {
            stateSrack.Peek()?.OnExit();
        }
        stateSrack.Push(states[type]);
        stateSrack.Peek()?.OnEnter();
    }

    public void Pop()
    {
        stateSrack.Pop();
        stateSrack.Peek()?.OnEnter();
        
    }
}
