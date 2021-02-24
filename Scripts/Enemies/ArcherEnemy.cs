using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : Enemy
{
    [Header("Attack Info")]
    public PoolBullet poolBullet;
    public Transform bulletPoint;
    public float multiplierOfArrow;

    public void RangeAttack()
    {        
        PoolObject arrow=PoolManager.Instance.UsePoolObject(poolBullet, bulletPoint.position, bulletPoint.rotation);
        //FindObjectOfType<AudioManager>().PlaySound("ArrowShoot");
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, Random.Range(0.35f, 0.65f)) * multiplierOfArrow;
        arrow.GetComponent<PoolBullet>().damageableLayer = towerLayer;
        arrow.GetComponent<PoolBullet>().damage = damage;
    }
}
