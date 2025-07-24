using UnityEngine;


[System.Serializable]
public class Hand
{

    public Card[] cards = new Card[3]; // Array to hold the cards in hand
    public Transform[] positions = new Transform[3]; // Positions for the cards in hand

    public string[] animNames = new string[3]; // Animation names for each card

    public bool isplayer;

    public void RemoveCard(Card card)
    {
        for (int i = 0; i < 3; i++)
        {
            if (cards[i] == card)
            {
                GameObject.Destroy(cards[i].gameObject); // Destroy the card GameObject
                cards[i] = null; // Remove the card from the hand
                if (isplayer)
                    GameController.instance.playerDeck.DealCard(this);
                else
                    GameController.instance.enemyDeck.DealCard(this);
                break;
            }
        }

    }

}
