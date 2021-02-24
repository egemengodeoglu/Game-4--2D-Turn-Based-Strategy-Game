using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    public EnemyRangeAttackState enemyRangeAttackState;
    public EnemyMeleeAttackState enemyMeleeAttackState;
    public EnemyMagicAttackState enemyMagicAttackState;
    public EnemyBombAttackState enemyBombAttackState;
    public EnemyIdleState enemyIdleState;
    public EnemyWalkState enemyWalkState;
    public EnemyDieState enemyDieState;
    public EnemyFreezeState enemyFreezeState;

    [HideInInspector]
    public Enemy enemy;
    [HideInInspector]
    public Animator anim; //0-> Die Anim    1-> Attack Anim    2->Idle Anim    3-> Run Anim
    [HideInInspector]
    public bool isWaiting;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        
        enemyIdleState = new EnemyIdleState(this);
        enemyWalkState = new EnemyWalkState(this);
        enemyRangeAttackState = new EnemyRangeAttackState(this);
        enemyMeleeAttackState = new EnemyMeleeAttackState(this);
        enemyMagicAttackState = new EnemyMagicAttackState(this);
        enemyBombAttackState = new EnemyBombAttackState(this);
        enemyFreezeState = new EnemyFreezeState(this);
        enemyDieState = new EnemyDieState(this);
        isWaiting = false;
    }

    void OnEnable()
    {
        SetState(enemyWalkState);
    }

    void Update()
    {
        currentState.Tick();
        if (enemy.IsDead)
        {
            SetState(enemyDieState);
        }else if (enemy.isFreeze)
        {
            SetState(enemyFreezeState);
        }
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public IEnumerator Waiter(float time)
    {
        isWaiting = false;
        yield return new WaitForSeconds(time);
        isWaiting = true;
    }

    public void ChooserAttackType()
    {
        switch (enemy.enemyType)
        {
            case EnemyType.MeleeAttacker:
                SetState(enemyMeleeAttackState);
                break;
            case EnemyType.RangedAttacker:
                SetState(enemyRangeAttackState);
                break;
            case EnemyType.MagicAttacker:
                SetState(enemyMagicAttackState);
                break; ;
            case EnemyType.SuicederAttacker:
                SetState(enemyBombAttackState);
                break;
        }
    }
}
