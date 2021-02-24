using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Interfaces;
using UnityEngine;

abstract public class Enemy : PoolObject, IDamageable, IDamageDealer
{
    public EnemyType enemyType;

    [Header("Enemy Info")]
    public float maxHealth = 100;
    public float speed;
    public float range, attackDistanceRandom;
    public int maxDamage;

    [HideInInspector]
    public int damage, maxHealth2;
    
    public LayerMask towerLayer;
    public GameObject healthbar;
    public Transform attackPointTransform;

    
    //Warrior -> speed:2 healt:150 damage:100 attackDistance:0,5 
    //Archer -> speed:2 healt:80 damage:60 attackDistance:10
    //Wizzard -> speed:2 healt:40 damage:30 attackDistance:15
    //Suiceder -> speed:4 healt:30 damage:200 attackDistance:0,1

    [HideInInspector]
    public bool control, isWalking, isFreeze;

    private bool isDie;
    protected float health;
    private float attackRange;

    [Header("Materials")]
    public Material freezedMaterial;
    public Material notFreezedMaterial;
    public PoolObject freezeEffect;


    protected void OnEnable()
    {
        IsDead = false;
        isWalking = false;
        isFreeze = false;
        maxHealth2 = (int)maxHealth + (int)(5 * WaweManager.Instance.waweCount);
        health = maxHealth2;
        damage = (maxDamage) + (int)(maxDamage * 0.07f * WaweManager.Instance.waweCount);
        healthbar.transform.parent.gameObject.SetActive(true);
        healthbar.transform.localScale = new Vector3(health/ maxHealth2, healthbar.transform.localScale.y,healthbar.transform.localScale.z);
        GetComponent<SpriteRenderer>().material = notFreezedMaterial;
        GetComponent<Collider2D>().enabled = true;
        attackRange = Random.Range(range - attackDistanceRandom, range);
    }

    public void EnemyMove()
    {
        Vector3 direction = new Vector3(-1, 0, 0);
        transform.position += direction * Time.deltaTime * speed;
    }

    public bool CanAttack()
    {
        if (Physics2D.OverlapCircleAll(attackPointTransform.position, attackRange, towerLayer).Length!=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        this.HideObject();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.transform.localScale = new Vector3(health/maxHealth2,healthbar.transform.localScale.y,healthbar.transform.localScale.z);
        //FindObjectOfType<AudioManager>().PlaySound("DamageTaken");
        if (health <= 0)
        {
            healthbar.transform.parent.gameObject.SetActive(false);
            IsDead = true;
        }
    }

    public void Freezer()
    {
        StartCoroutine(FreezerAnim());
    }

    public IEnumerator FreezerAnim()
    {
        GetComponent<StateMachine>().enabled = false;
        GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(4);
        GetComponent<StateMachine>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }


    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }

    public bool IsDead
    {
        get
        {
            return isDie;
        }
        set
        {
            isDie = value;
        }
    }

    public IEnumerator Waiter(float waitTime)
    {
        control = false;
        yield return new WaitForSeconds(waitTime);
        control = true;
    }

}
