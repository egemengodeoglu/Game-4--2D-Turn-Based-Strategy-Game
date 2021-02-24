using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CardManager : MonoBehaviour
{
    public CardScriptableObject scriptableObject;
    public GameObject[] cardPanels;
    private Vector2 deckPosition, showPosition;
    private int cardsPositionX;
    private int totalCardSize;
    private System.Random rnd;
    public Action<CardType, int> UpdateSkills;

    private void Awake()
    {
        scriptableObject.SetDefaultValue();
        TotalCard();
        rnd = new System.Random();
        deckPosition = new Vector2(-480, 0);
        cardsPositionX = -220;
        showPosition = new Vector2(820, 0);
        
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(CardShower(cardPanels[i], i, (i + 1) * 0.5f));
        }

    }
    public void CardChoosed(GameObject card)
    {
        StartCoroutine(CardChooseAnim(card));
    }

    public void TotalCard()
    {
        totalCardSize = 0;
        for (int i = 0; i < scriptableObject.cards.Count; i++)
        {
            totalCardSize += scriptableObject.cards[i].count;
        }
    }

    public Card RandomCard()
    {
        int random = rnd.Next(1, totalCardSize + 1);
        totalCardSize--;
        int tmp = 0;
        foreach (Card card in scriptableObject.cards)
        {
            if ((tmp + card.count) >= random)
            {
                card.count--;
                return card;
            }
            else
            {
                tmp += card.count;
            }
        }
        return null;
    }


    public IEnumerator CardShower(GameObject cardPanel, int index, float time)
    {
        CardPanelScript cps = cardPanel.GetComponent<CardPanelScript>();
        RectTransform crt = cardPanel.GetComponent<RectTransform>();

        Card tmpCard = RandomCard();
        //cps.cardTypeText.text = tmpCard.name.ToString();
        //cps.destcriptionText.text = tmpCard.description.ToString();
        cps.destcriptionText.text = tmpCard.name.ToString();
        cps.costText.text = tmpCard.cost.ToString();
        cps.image.sprite = tmpCard.sprite;
        cps.typeOfCard = tmpCard.type;
        cps.cost = tmpCard.cost;

        cps.frontPanel.SetActive(false);
        cps.backPanel.SetActive(true);
        crt.DOScale(new Vector3(1f, 1f, 1f), 0.001f);
        cps.frontPanel.GetComponent<Button>().interactable = false;
        crt.anchoredPosition = deckPosition;
        crt.DOAnchorPos(new Vector2(cardsPositionX+index*235,0), time);
        crt.DORotate(new Vector3(0, 90, 0), 0.5f).SetDelay(time);
        crt.DORotate(new Vector3(0, -90, 0), 0.001f).SetDelay(time + 0.5f);
        crt.DORotate(new Vector3(0, 0, 0), 0.5f).SetDelay(time + 0.5f);
        yield return new WaitForSeconds(time + 0.43f);
        cps.frontPanel.SetActive(true);
        cps.backPanel.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        cps.frontPanel.GetComponent<Button>().interactable = true;
    }

    public IEnumerator CardChooseAnim(GameObject go)
    {
        CardPanelScript cps = go.GetComponent<CardPanelScript>();
        RectTransform crt = go.GetComponent<RectTransform>();
        UpdateSkills.Invoke(cps.typeOfCard, cps.cost);
        cps.frontPanel.GetComponent<Button>().interactable = false;
        crt.DOAnchorPos(showPosition, 1.5f);
        crt.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.5f).SetDelay(1);
        yield return new WaitForSeconds(2.5f);
        go.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(CardShower(go, cps.cardId, cps.cardId * 0.5f));
        go.SetActive(true);
    }


}