using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardEnemy: Enemy
{
    public PoolMagic poolMagic;
    public Transform magicPoint;
    public float multiplierOfMagic;

    public void WizzardAttack()
    {
        PoolObject magic=PoolManager.Instance.UsePoolObject(poolMagic, magicPoint.position, magicPoint.rotation);
        magic.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0) * multiplierOfMagic;
        magic.GetComponent<PoolMagic>().damageableLayer = towerLayer;
        magic.GetComponent<PoolMagic>().damage = damage;
    }
}
