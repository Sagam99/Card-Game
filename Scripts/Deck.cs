using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class Deck
{
    public List<CardData> cardDatas = new List<CardData>();

    public void Create()

    {
        // first create a list of card data for the pack
        List<CardData> cardDataInOrder = new List<CardData>();
        foreach (CardData cardData in GameController.instance.cards)
        {
            for (int i = 0; i < cardData.numberInDeck; i++)
                cardDataInOrder.Add(cardData);
        }





        // then randomize the list
        while (cardDataInOrder.Count > 0)
        {
            int randomIndex = Random.Range(0, cardDataInOrder.Count);
            cardDatas.Add(cardDataInOrder[randomIndex]);
            cardDataInOrder.RemoveAt(randomIndex);
        }
    }

    private CardData RandomCard()
    {

        CardData result = null;

        if (cardDatas.Count == 0)
            Create();


        result = cardDatas[0]; // get the first card in the deck
        cardDatas.RemoveAt(0); // remove the card from the deck

        return result;
    }

    private Card CreateNewCard(Vector3 position, string animName)
    {

        GameObject newCard = GameObject.Instantiate(GameController.instance.cardPrefab, GameController.instance.canvas.gameObject.transform); // Instantiate the card prefab

        newCard.transform.position = position; // Set the position of the card
        Card card = newCard.GetComponent<Card>(); // Get the Card component from the prefab
        if (card)
        {
            card.cardData = RandomCard(); // Assign a random card data to the card
            card.Initialise();

            Animator animator = newCard.GetComponentInChildren<Animator>();
            if (animator)
            {
                animator.CrossFade(animName,0);
            }

            else
            {
                Debug.LogError("No animator Found!");
            }
            return card; // Return the card component
        }


        else
        {
            Debug.LogError("No card component found!");
            return null;
        }
    }

    internal void DealCard(Hand hand)
    {
        for (int h = 0; h < 3; h++)
        {
            if (hand.cards[h] == null)
            {
                hand.cards[h] = CreateNewCard(hand.positions[h].position, hand.animNames[h]);
                return;            }
        }
    }

}
