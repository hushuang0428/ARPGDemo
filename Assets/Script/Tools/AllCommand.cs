using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 还有待研究
/// </summary>
public class MoveForwardCommand : ICommand
{
    private FSM fsm;


    public MoveForwardCommand(FSM fsm)
    {
        this.fsm = fsm;
    }
    
    public void Execute()
    {
        Move();
    }

    private void Move()
    {
        fsm.TransitionState(StateType.Move);
    }

    
}
