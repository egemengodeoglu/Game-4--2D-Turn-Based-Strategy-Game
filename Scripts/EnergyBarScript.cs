using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    public Slider energySlider;
    public TextMeshProUGUI costText;
    public int cost;

    #region Singleton 
    private static EnergyBarScript _instance;
    public static EnergyBarScript Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<EnergyBarScript>();
            return _instance;
        }
    }
    #endregion

    private void Start()
    {
        SetEnergy(100);
        costText.text = cost.ToString();
    }

    public void SetCost(int costValue)
    {
        energySlider.value -= 0.25f * costValue;
        cost -= costValue;
        costText.text = cost.ToString();
    }

    public void SetEnergy(int value)
    {
        energySlider.value += (float)value / 100;
        if (energySlider.value >= 1f)
        {
            energySlider.value = 1f;
            cost = 4;
        } 
        else if (energySlider.value >= 0.75f)
        {
            cost = 3;
        }
        else if (energySlider.value >= 0.5f)
        {
            cost = 2;
        }
        else if (energySlider.value >= 0.25f)
        {
            cost = 1;
        }
        else
        {
            cost = 0;
        }
        costText.text = cost.ToString();
    }

}
