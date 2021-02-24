using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
	public List<Card> cards;
	public List<DefaultCardValues> cardsDefaultValue;

    public void SetDefaultValue()
    {
        for (int i=0;i<cards.Count;i++)
        {
			cards[i].count = cardsDefaultValue[i].count;
		}
    }
}
[System.Serializable]
public class Card
{
	public string name;
	public string description;
	public Sprite sprite;
	public int cost;
	public int count;
	public CardType type;
}

[System.Serializable]
public class DefaultCardValues
{
	public string name;
	public int count;
}
