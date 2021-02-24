using Interfaces;
using UnityEngine;
using Time = UnityEngine.Time;

public class PoolBullet: PoolObject, IDamageDealer
{
    public PoolObject hitEffect;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public int damage;

    public LayerMask damageableLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable hitObj = col.GetComponentInParent<IDamageable>();
        if (hitObj != null)
        {
            if ((damageableLayer.value >> col.gameObject.layer) == 1)
            {
                ApplyDamage(hitObj);
                OnHideObject?.Invoke(this);
            }
        }
        else
        {
            OnHideObject?.Invoke(this);
        }
        
        //PoolManager.Instance.UsePoolObject(hitEffect, transform.position, Quaternion.identity);
    }
    

    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }
}
