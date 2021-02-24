using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageScript : MonoBehaviour
{
    public PoolObject hitEffect;
    public int damage;
    private Collider2D hit2D;
    public LayerMask damageableLayer;
    private Animator animator;
    public float range;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (CanAttack() != null)
        {
            animator.SetInteger("EnemyAnim", 1);
        }
        else
        {
            animator.SetInteger("EnemyAnim", 0);
        }
    }
    public void MagicAttack(){
        Collider2D [] colliders = Physics2D.OverlapCircleAll(hit2D.transform.position, 3f, damageableLayer);
        PoolManager.Instance.UsePoolObject(hitEffect, hit2D.transform.position, Quaternion.identity);
        foreach(Collider2D collider in colliders){
            Debug.Log(collider.name);
            collider.GetComponent<IDamageable>().TakeDamage(damage);
        }

        
    }
    private Collider2D CanAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, damageableLayer);
        float shortestDistance = Mathf.Infinity;
        Collider2D target = null;
        
        foreach(Collider2D hit in hits)
        {
            if(Vector2.Distance(transform.position, hit.transform.position) < shortestDistance) { 
                target = hit;
                shortestDistance = Vector2.Distance(transform.position, hit.transform.position);
            }
        }
        hit2D = target;
        return target;
    }
}
