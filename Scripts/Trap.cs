using Interfaces;
using UnityEngine;

public class Trap : MonoBehaviour, IDamageDealer
{
    public int damage;
    public LayerMask damageableLayer;
    private Animator anim;
    private bool control;

    private void Awake()
    {
        control = false;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!control)
        {
            IDamageable hitObj = collision.GetComponent<IDamageable>();
            if (hitObj != null && (damageableLayer.value >> collision.gameObject.layer) == 1)
            {
                 anim.SetBool("TrapAnim", true);
                 control = true;
                 DamageEnemy();
            }
        }

    }

    public void DamageEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.6f, damageableLayer);
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    public void TrapFinisher()
    {
        control = false;
        anim.SetBool("TrapAnim", false);
    }

    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }


}
