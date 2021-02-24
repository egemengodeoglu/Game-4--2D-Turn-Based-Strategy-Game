using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CardPanelScript: MonoBehaviour
{
    public int cardId;
    public GameObject backPanel, frontPanel;
    public TextMeshProUGUI cardTypeText,destcriptionText,costText;
    public Image image, backGroundImage;
    public Material selectableMaterial, notSelectableMaterial;
    [HideInInspector]
    public CardType typeOfCard;
    public int cost;

    private void Update()
    {
        if ( EnergyBarScript.Instance.cost >= cost)
        {
            if (backGroundImage.material != selectableMaterial)
            {
                backGroundImage.material = selectableMaterial;
            }
            frontPanel.GetComponent<Button>().interactable = true;
        }
        else
        {
            if (backGroundImage.material != notSelectableMaterial)
            {
                backGroundImage.material = notSelectableMaterial;
            }
            frontPanel.GetComponent<Button>().interactable = false; ;
        }
    }
}
