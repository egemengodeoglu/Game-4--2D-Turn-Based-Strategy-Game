using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageDealer
{
    public PoolObject firewall, fireball, sphereBlastEffect;
    public Transform transformNear, transformFar, transformAll;
    public AudioClip fireballSound, sphereBlastSound, freezerSound, ultiMageSound;

    [HideInInspector]
    public CardType ultiCard;
    [HideInInspector]
    public bool isUlti;
    
    [Header("Attack Info")]
    public int damage;
    public float attackSpeed,range;
    
    public LayerMask damageableLayer;

    [HideInInspector]
    public Animator animator;
    private Collider2D hit2D;
    private GameManagerScript gameManagerScript;
    void Start()
    {
        attackSpeed = 1;
        gameManagerScript = FindObjectOfType<GameManagerScript>();
        gameManagerScript.MageUlti += MageUlti;
        animator = GetComponent<Animator>();
    }
   
    void Update()
    {
        if (isUlti)
        {
            animator.SetFloat("PlayerAnim", -1);
        }
        else if (CanAttack() != null)
        {
            animator.SetFloat("PlayerAnim", attackSpeed);
        }
        else
        {
            animator.SetFloat("PlayerAnim", 0.5f);
        }
    }
    public void MagicAttack() 
    {
        AudioManager.Instance.PlaySound(sphereBlastSound);
        if (Vector3.Distance(hit2D.transform.position, transform.position) > 10)
        {
            AttackFunction(sphereBlastEffect, 1f, hit2D.transform.position, damage);
        }
        else
        {
            AttackFunction(sphereBlastEffect, 1f, hit2D.transform.position, damage * 2);
        }
    }
       

    public void UltiFinisher()
    {
        isUlti = false;
    }
    
    public void MageUlti(CardType typeOfCard)
    {
        ultiCard = typeOfCard;
        animator.SetFloat("PlayerAnim", -1);
        AudioManager.Instance.PlaySound(ultiMageSound);
        isUlti = true;
    }
    
    public void UltiChooser()
    {
        switch (ultiCard)
        {
            case CardType.FIREBALL_NEAR:
                PoolManager.Instance.UsePoolObject(firewall, transformNear.position, Quaternion.identity);
                break;
            case CardType.FIREBALL_FAR:
                PoolManager.Instance.UsePoolObject(firewall, transformFar.position, Quaternion.identity);
                break;
            case CardType.FIREBALL:
                StartCoroutine(Explosion());
                break;
            case CardType.FREEZER:
                Freezer();
                break;
        }
        
        
    }

    public void AttackFunction(PoolObject effect, float rangeofEffect, Vector3 transform,int damage)
    {
        PoolManager.Instance.UsePoolObject(effect, transform, Quaternion.identity);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform, rangeofEffect, damageableLayer);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.GetComponent<IDamageable>() != null)
            {
                enemy.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
    }

    public void Freezer()
    {
        AudioManager.Instance.PlaySound(freezerSound);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transformFar.position, 20f, damageableLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>().isFreeze = true;
        }
    }

    public IEnumerator Explosion()
    {
        Vector3 tmp = transformAll.position;
        AudioManager.Instance.PlaySound(fireballSound);
        AttackFunction(fireball, 2.5f, tmp,1500);
        yield return new WaitForSeconds(0.2f);
        tmp.x +=5;
        AudioManager.Instance.PlaySound(fireballSound);
        AttackFunction(fireball, 2.5f, tmp, 1500);
        yield return new WaitForSeconds(0.2f);
        tmp.x += 5;
        AudioManager.Instance.PlaySound(fireballSound);
        AttackFunction(fireball, 2.5f, tmp, 1500);
        yield return new WaitForSeconds(0.2f);
        tmp.x += 5;
        AudioManager.Instance.PlaySound(fireballSound);
        AttackFunction(fireball, 2.5f, tmp, 1500);
        yield return new WaitForSeconds(0.2f);
        tmp.x += 5;
        AudioManager.Instance.PlaySound(fireballSound);
        AttackFunction(fireball, 2.5f, tmp, 1500);
        yield return new WaitForSeconds(0.2f);
        tmp.x += 5;
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

    public void ApplyDamage(IDamageable obj)
    {
        obj.TakeDamage(damage);
    }

}
