using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezeState : State
{
    private PoolObject effect;
    public EnemyFreezeState(StateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void OnStateEnter()
    {
        stateMachine.enemy.isFreeze = false;
        stateMachine.anim.enabled = false;
        stateMachine.enemy.GetComponent<SpriteRenderer>().material = stateMachine.enemy.freezedMaterial;
        stateMachine.enemy.StartCoroutine(stateMachine.enemy.Waiter(5f));
        effect=PoolManager.Instance.UsePoolObject(stateMachine.enemy.freezeEffect, stateMachine.enemy.transform.position, Quaternion.identity);
    }

    public override void Tick()
    {
        if (stateMachine.enemy.control)
        {
            stateMachine.enemy.isFreeze = false;
            stateMachine.SetState(stateMachine.enemyWalkState);
        }

    }

    public override void OnStateExit()
    {
        //effect.OnHideObject(effect);
        stateMachine.anim.enabled = true;
        stateMachine.enemy.GetComponent<SpriteRenderer>().material = stateMachine.enemy.notFreezedMaterial;
    }
}
