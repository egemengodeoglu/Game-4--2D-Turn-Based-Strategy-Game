using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TowerInfoPanelScript : MonoBehaviour
{
    public TextMeshProUGUI attack, speed, range;
    private int attackCount, speedCount, rangeCount;
    public int max;

    private void Start()
    {
        attack.text = "1";
        range.text = "1";
        speed.text = "1";
        attackCount = 1;
        rangeCount = 1;
        speedCount = 1;
        max = 20;
    }

    public void UpgradeAttackText()
    {
        if ((++attackCount)==max)
        {
            attack.text = "Max";
        }
        else
        {
            attack.text = attackCount.ToString();
        }
    }

    public void UpgradeRangeText()
    {
        if ((++rangeCount) == max)
        {
            range.text = "Max";
        }
        else
        {
            range.text = rangeCount.ToString();
        }
    }

    public void UpgradeSpeedText()
    {
        if ((++speedCount) == max)
        {
            speed.text = "Max";
        }
        else
        {
            speed.text = speedCount.ToString();
        }
    }

}
