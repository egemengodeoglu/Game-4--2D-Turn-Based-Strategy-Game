using Interfaces;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Enemy
{
    public void MeleeAttack()
    {
        
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPointTransform.position, 0.5f, towerLayer);
        if (hit.Length != 0)
        {
            if (hit[0].GetComponentInParent<Tower>() != null)
            {
                hit[0].GetComponentInParent<Tower>().gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            }else if(hit[0].GetComponent<FriendlyWarrior>() != null)
            {
                hit[0].GetComponent<FriendlyWarrior>().gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
    }
}
