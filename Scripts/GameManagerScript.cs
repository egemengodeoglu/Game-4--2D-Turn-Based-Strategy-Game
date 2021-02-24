using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private CardManager cardManager;
    private Tower tower;
    public Action<String, int, int> ChangePanel;
    public Action<CardType> MageUlti;

    //public static bool GameIsPaused = false;

    public Action<CardType> TowerSkills;
    void Start()
    {
        tower = GameObject.FindObjectOfType<Tower>();
        tower.OnTowerEvent += UpdateHealthBar;
        cardManager = GameObject.FindObjectOfType<CardManager>();
        cardManager.UpdateSkills += CardChoosedFunction;
    }

    public void CardChoosedFunction(CardType typeOfCard, int cost)
    {
        switch (typeOfCard)
        {
            case CardType.FIREBALL:
                MageUlti.Invoke(typeOfCard);
                break;
            case CardType.FIREBALL_NEAR:
                MageUlti.Invoke(typeOfCard);
                break;
            case CardType.FIREBALL_FAR:
                MageUlti.Invoke(typeOfCard);
                break;
            case CardType.FREEZER:
                MageUlti.Invoke(typeOfCard);
                break;
            case CardType.TOWERSPEEDBOOSTER:
                TowerSkills.Invoke(typeOfCard);
                ChangePanel.Invoke("SpeedUpdate", 0, 0);
                break;
            case CardType.TOWERRANGE:
                TowerSkills.Invoke(typeOfCard);
                ChangePanel.Invoke("RangeUpdate", 0, 0);
                break;
            case CardType.TOWERDAMAGEBOOSTER:
                TowerSkills.Invoke(typeOfCard);
                ChangePanel.Invoke("AttackUpdate", 0, 0);
                break;
            default:
                TowerSkills.Invoke(typeOfCard);
                break;
        }
        ChangePanel.Invoke("UpdateEnergyBar", cost,0);
    }
    
    public void Death(){
        ChangePanel.Invoke("UpdateHealtBar",0,0);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        ChangePanel.Invoke("Resume", 0, 0);
        Time.timeScale = 1f;
        //GameIsPaused = false;
    }
    public void Pause()
    {
        ChangePanel.Invoke("Pause", 0, 0);
        Time.timeScale = 0f;
        //GameIsPaused = true;
    }

    public void Retry()
    {
        ChangePanel.Invoke("Retry", 0, 0);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    public void UpdateHealthBar(String data, int health, int maxHealth)
    {
        if(health <= 0){
            Death();
        }
        else
            ChangePanel.Invoke(data, health, maxHealth);
    }
}
