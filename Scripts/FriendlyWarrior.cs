using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyWarrior : PoolObject, IDamageable, IDamageDealer
{
    public float maxHealth;
    public int damage;
    public float range;
    public LayerMask enemyLayer;
    private float health;
    private bool isDie;
    public float speed;
    public Transform attackPointTransform;
    public GameObject healthbar;
    private Animator animator;
    public AudioClip hitAudio;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnEnable()
    {
        IsDead = false;
        health = maxHealth;
        GetComponent<BoxCollider2D>().enabled = true;
        healthbar.transform.parent.gameObject.SetActive(true);
        healthbar.transform.localScale = new Vector3(health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }

    private void Update()
    {
        if (!IsDead)
        {
            if (CanAttack() == true)
            {
                animator.SetInteger("EnemyAnim", 1);
            }
            else
            {

                animator.SetInteger("EnemyAnim", 0);
                EnemyMove();
            }
        }
        else
        {
            animator.SetInteger("EnemyAnim", 2);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public bool CanAttack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPointTransform.position, range, enemyLayer);
        if (hit.Length != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EnemyMove()
    {
        Vector3 direction = new Vector3(1, 0, 0);
        transform.position += direction * Time.deltaTime * speed;
    }

    public void MeleeAttack()
    {
        AudioManager.Instance.PlaySound(hitAudio);
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPointTransform.position, 0.5f, enemyLayer);
        if (hit.Length != 0)
            hit[0].GetComponent<IDamageable>().TakeDamage(30);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.transform.localScale = new Vector3(health / maxHealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
        if (health <= 0)
        {
            healthbar.transform.parent.gameObject.SetActive(false);
            IsDead = true;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDie;
        }
        set
        {
            isDie = value;
        }
    }

    public void Die()
    {
        this.HideObject();
    }

    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }
}
