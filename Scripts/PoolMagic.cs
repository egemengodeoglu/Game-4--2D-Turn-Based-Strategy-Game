using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMagic : PoolObject, IDamageDealer
{
    public int damage;

    public LayerMask damageableLayer,arrowLayer;
   


    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable hitObj = col.GetComponent<IDamageable>();
        if (hitObj != null)
        {
            
            if ((damageableLayer.value >> col.gameObject.layer) == 1)
            {
                ApplyDamage(hitObj);
                OnHideObject?.Invoke(this);
            }
        }
        else if((arrowLayer.value >> col.gameObject.layer) != 1)
        {
            OnHideObject?.Invoke(this);
        }
    }

    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }
}
