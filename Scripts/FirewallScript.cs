using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallScript : PoolObject
{
    [HideInInspector]
    public bool control;
    public int damage;
    public float rangeofEffect;
    public float timeOfEffect;
    public LayerMask damageableLayer;
    private AudioSource auidoSource;
    private void OnEnable()
    {
        auidoSource = GetComponent<AudioSource>();
        StartCoroutine(FireWaiter());
        auidoSource.Play();
    }
    private void Update()
    {
        if (!control)
        {
            AttackFunction(rangeofEffect, transform.position, damage);
        }
        else
        {
            auidoSource.Stop();
            OnHideObject(this);
        }
    }

    private IEnumerator FireWaiter()
    {
        control = false;
        yield return new WaitForSeconds(timeOfEffect);
        control = true;
    }

    public void AttackFunction(float rangeofEffect, Vector3 transform, int damage)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform, rangeofEffect, damageableLayer);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.GetComponent<IDamageable>() != null)
            {
                enemy.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
    }
}
