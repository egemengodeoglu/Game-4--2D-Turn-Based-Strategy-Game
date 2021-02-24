using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private GameManagerScript panelManagement;
    public GameObject pausePanel, diePanel;
    public HealthBarScript healthBar;
    public EnergyBarScript energyBar;

    public TowerInfoPanelScript towerInfoPanel;
    void Start()
    {
        panelManagement = GameObject.FindObjectOfType<GameManagerScript>();
        panelManagement.ChangePanel += ChangePanel;
        diePanel.gameObject.SetActive(false);
    }

    private void ChangePanel(string chooser, int data1, int data2)
    {
        switch (chooser)
        {
            case "Resume":
                pausePanel.SetActive(false);
                break;
            case "Pause":
                pausePanel.SetActive(true);
                break;
            case "Retry":
                pausePanel.SetActive(false);
                diePanel.SetActive(false);
                break;
            case "UpdateHealtBar":
                if (data1 <= 0)
                {
                    healthBar.SetHealth(0, data2);
                    diePanel.SetActive(true);
                    diePanel.GetComponent<DiePanelScript>().waveCount.text = "Wave Count : "+ WaweManager.Instance.waweCount.ToString();
                    Time.timeScale = 0f;
                }
                else
                {
                    healthBar.SetHealth(data1, data2);
                }
                break;
            case "UpdateEnergyBar":
                energyBar.SetCost(data1);
                break;
            case "AttackUpdate":
                towerInfoPanel.UpgradeAttackText();
                break;
            case "RangeUpdate":
                towerInfoPanel.UpgradeRangeText();
                break;
            case "SpeedUpdate":
                towerInfoPanel.UpgradeSpeedText();
                break;
            default:
                break;
        }

    }
}
