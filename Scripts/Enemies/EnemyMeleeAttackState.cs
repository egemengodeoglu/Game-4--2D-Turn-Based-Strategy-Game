using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : State
{
    public EnemyMeleeAttackState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim", 1);

    }

    public override void Tick()
    {
        if (!stateMachine.enemy.CanAttack())
        {
            stateMachine.SetState(stateMachine.enemyWalkState);
        }
    }

    public override void OnStateExit()
    {

    }



}
