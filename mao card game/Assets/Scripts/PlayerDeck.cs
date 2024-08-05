using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{

    //public Dictionary<string, GameObject> cards = new Dictionary<string, GameObject>();
    //public List<string> cards = new List<string>();
    public List<Card> cards = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCard(string card, GameObject cardObj)
    {
        Card currCard = new Card(card, cardObj);
        cards.Add(currCard);
    }

    public void removeCard()
    {

    }
}

public struct Card
{

    private string cardName;
    private GameObject cardObj;

    public Card(string cardName, GameObject cardObj)
    {
        this.cardName = cardName;
        this.cardObj = cardObj;
    }
    public string getName()
    {
        return cardName;
    }

    public GameObject getCardObj()
    {
        return cardObj;
    }

    public void setName(string cardName)
    {
        this.cardName = cardName;
    }

    public void setCardObj(GameObject cardObj)
    {
        this.cardObj = cardObj;
    }
}

