using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable
{

    [Header("Player Info")]
    public int maxHealth;
    private int health;
    private bool isDie;

    public Action<String,int, int> OnTowerEvent;
    [Header("Tower Defencers")]
    public List<TowerGuard> towerGuards;
    public List<GameObject> towerTraps;
    private Player player;
    public Transform warriorSpawnPoint;
    public FriendlyWarrior warrior;
    private GameManagerScript gameManager;
    [Header("Effects")]
    public PoolObject effectHeal;

    void Start()
    {
        player = FindObjectOfType<Player>();
        health = maxHealth;
        gameManager = FindObjectOfType<GameManagerScript>();
        gameManager.TowerSkills += UpdateSkills;
        OnTowerEvent.Invoke("UpdateHealtBar", health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnTowerEvent.Invoke("UpdateHealtBar",health,maxHealth);
        if (health <= 0)
        {
            IsDead = true;
            Die();
        }
    }

    public void Die()
    {
        foreach (TowerGuard tg in towerGuards)
        {
            if(tg.gameObject.activeSelf)
                tg.animator.SetBool("isDead", true);
        }
    }

    public void UpdateSkills(CardType typeOfCard)
    {
        switch (typeOfCard)
        {
            case CardType.TOWERDAMAGEBOOSTER:
                foreach (TowerGuard tg in towerGuards)
                {
                    tg.damage += 20;
                }
                player.damage += (int)(player.damage * 0.3f);
                warrior.damage += (int)(warrior.damage * 0.3f);
                break;
            case CardType.TOWERSPEEDBOOSTER:
                foreach (TowerGuard tg in towerGuards)
                {
                    tg.attackspeed += 0.05f;
                }
                player.attackSpeed += 0.1f;
                break;
            case CardType.TOWERRANGE:
                foreach (TowerGuard tg in towerGuards)
                {
                    tg.range += 1f;
                }
                break;
            case CardType.TOWERHEAL:
                health += (int) (maxHealth * 0.5f);
                if(health > maxHealth)
                {
                    health = maxHealth;

                }
                OnTowerEvent.Invoke("UpdateHealtBar", health, maxHealth);
                break;
            case CardType.TOWERMAXHEALTH:
                maxHealth += 100;
                warrior.maxHealth += 50;
                OnTowerEvent.Invoke("UpdateHealtBar", health, maxHealth);
                break;
            case CardType.TOWERGUARD:
                foreach(TowerGuard tg in towerGuards)
                {
                    if (!tg.gameObject.activeSelf)
                    {
                        tg.gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            case CardType.TOWERTRAP:
                foreach (GameObject tt in towerTraps)
                {
                    if (!tt.gameObject.activeSelf)
                    {
                        tt.gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            case CardType.TOWERWARRİOR:
                PoolManager.Instance.UsePoolObject(warrior, warriorSpawnPoint.position,Quaternion.identity);
                break;
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

}
