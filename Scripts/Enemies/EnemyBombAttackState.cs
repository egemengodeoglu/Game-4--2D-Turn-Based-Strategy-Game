using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombAttackState : State
{
    public EnemyBombAttackState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        stateMachine.enemy.StartCoroutine(stateMachine.enemy.Waiter(0.7f));
        stateMachine.enemy.GetComponent<SpriteRenderer>().material = stateMachine.enemy.GetComponent<SuicederEnemy>().shake;
    }

    public override void Tick()
    {
        if(stateMachine.enemy.control){
            stateMachine.enemy.GetComponent<SpriteRenderer>().material = stateMachine.enemy.notFreezedMaterial;
            stateMachine.SetState(stateMachine.enemyDieState);
        }
    }

    public override void OnStateExit()
    {
        
    }



}
