using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicederEnemy : Enemy
{
    public Material shake;

    public void Explosion(){
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.8f, towerLayer);
        foreach(Collider2D hit in hits)
        {
            hit.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
