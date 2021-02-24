using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    public EnemyIdleState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim", 2);
        stateMachine.enemy.StartCoroutine(stateMachine.enemy.Waiter(0));
    }

    public override void Tick()
    {
        if (stateMachine.enemy.control)
        {
            if (stateMachine.enemy.CanAttack())
            {
                stateMachine.ChooserAttackType();
            }
            else
            {
                stateMachine.SetState(stateMachine.enemyWalkState);
            }
        }
    }

    public override void OnStateExit()
    {
    }

    
}
