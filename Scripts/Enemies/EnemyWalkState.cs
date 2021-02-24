using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : State
{
    public EnemyWalkState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim", 3);
        stateMachine.enemy.isWalking = true;
    }

    public override void Tick()
    {
        if (stateMachine.enemy.CanAttack())
        {
            stateMachine.ChooserAttackType();
        }
        else
        {
            stateMachine.enemy.EnemyMove();
        }
    }

    public override void OnStateExit()
    {
        stateMachine.enemy.isWalking = false;
    }
}
