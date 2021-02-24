using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : State
{
    public EnemyDieState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim",0);
        stateMachine.enemy.GetComponent<BoxCollider2D>().enabled = false;
        stateMachine.enemy.IsDead = false;
        EnergyBarScript.Instance.SetEnergy(10);
    }

    public override void Tick()
    {

    }

    public override void OnStateExit()
    {
        
    }
}
