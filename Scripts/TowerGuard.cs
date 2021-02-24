using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGuard : MonoBehaviour, IDamageDealer
{
    [Header("Arrow")]
    public PoolBullet poolBullet;
    public Transform bulletSpawnTransform;
    public LayerMask damageableLayer;
    public float attackspeed, minDistance;
    public List<ArrowVariable> arrowVariables;
    public float multiplierOfVelocity, yOfVelocity, diveder;
    public float multiplierOfEnemySpeed;
    public Transform effectPoint;
    public AudioClip effectSound;

    [Header("TowerGuard Info")]
    public float range;
    public int damage;

    public class ArrowVariable
    {
        public float distance, multiplierOfVelocity, yOfVelocity;
    }

    private Collider2D hit2D;
    [HideInInspector]
    public Animator animator;
    
    private void Awake() {
        animator = GetComponent<Animator>();
        attackspeed = 1;
    }

    public void SendArrow()
    {
        if (hit2D != null)
        {
            PoolBullet arrow = PoolManager.Instance.UsePoolObject(poolBullet, bulletSpawnTransform.position, bulletSpawnTransform.rotation) as PoolBullet;
            AudioManager.Instance.PlaySound(effectSound);
            arrow.damageableLayer = damageableLayer;
            arrow.damage = damage;
            float distance = Vector2.Distance(transform.position, hit2D.GetComponent<Transform>().position);
           
            if (hit2D.gameObject.GetComponent<Enemy>().isWalking)
            {
                arrow.rb.velocity = new Vector2(0.5f, yOfVelocity) * (multiplierOfVelocity + (distance / diveder) - multiplierOfEnemySpeed * hit2D.gameObject.GetComponent<Enemy>().speed);
            }
            else
            {
                arrow.rb.velocity = new Vector2(0.5f, yOfVelocity) * (multiplierOfVelocity + (distance / diveder));
            }
        }

    }
    void Update()
    {
        if (!animator.GetBool("isDead"))
        {
            if (CanAttack() != null)
            {
                animator.SetFloat("TowerGuardAnim", attackspeed);
            }
            else
            {
                animator.SetFloat("TowerGuardAnim", 0.5f);
            }
        }
        
    }

    private Collider2D CanAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, damageableLayer);
        float shortestDistance = Mathf.Infinity;
        Collider2D target = null;
        
        foreach(Collider2D hit in hits)
        {
            if(Vector2.Distance(transform.position, hit.transform.position) < shortestDistance && Vector2.Distance(transform.position, hit.transform.position) > minDistance) { 
                target = hit;
                shortestDistance = Vector2.Distance(transform.position, hit.transform.position);
            }
        }
        hit2D = target;
        return target;
    }

    void OnDrawGizmos()
    {
        /*Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, minDistance);*/
    }
    ///////////////////////////////////////////////////////////////////////////////

    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }

    
}
