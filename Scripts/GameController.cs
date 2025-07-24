using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static public GameController instance = null;

    public Deck playerDeck = new Deck(); // Player's deck
    public Deck enemyDeck = new Deck(); // Enemy's deck

    public Hand playerHand = new Hand(); // Player's hand
    public Hand enemyHand = new Hand(); // Enemy's hand

    public Player player = null; // Reference to the player
    public Player enemy = null; // Reference to the enemy

    public List<CardData> cards = new List<CardData>();
    public Sprite[] healthNumbers = new Sprite[10];
    public Sprite[] damageNumbers = new Sprite[10];


    public GameObject cardPrefab = null; // Prefab for card objects

    public Canvas canvas = null; // Reference to the main canvas

    public bool isPlayable = false; // Flag to check if the game is playable

    public GameObject effectFromLeftPrefab = null; // Effect prefab for left side
    public GameObject effectFromRightPrefab = null; // Effect prefab for right side

    public Sprite fireballImage = null;
    public Sprite iceBallImage = null;
    public Sprite multifireballImage = null;
    public Sprite multiIceballImage = null;
    public Sprite FireAndIceImage = null;


    public void Awake()
    {
        instance = this;

        playerDeck.Create();
        enemyDeck.Create();

        StartCoroutine(DealHands()); // Start dealing initial hands to player and enemy
    }

    public void Quit()

    {

        SceneManager.LoadScene(0); // Load the main menu scene (index 0)
    }

    public void SkipTurn()
    {
        Debug.Log("Skip Turn");
    }

    internal System.Collections.IEnumerator DealHands() // Method to deal initial hands to player and enemy
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before dealing hands
        for (int t = 0; t < 3; t++)
        {
            playerDeck.DealCard(playerHand); // Deal a card to the player's hand
            enemyDeck.DealCard(enemyHand); // Deal a card to the enemy's hand
            yield return new WaitForSeconds(1); // Wait for 1 second between dealing cards

        }
        isPlayable = true; // Set the game to playable after dealing hands
    }

    internal bool UseCard(Card card, Player usingOnPlayer, Hand fromHand)
    {

        
        //remove card from hand
        // deal replace card
        if (!CardValid(card, usingOnPlayer, fromHand)) // Check if the card is valid
            return false;

        isPlayable = false; // Set the game to not playable while using the card

        CastCard(card, usingOnPlayer, fromHand); // Cast the card

        fromHand.RemoveCard(card); // Remove the card from the hand

        return true; // Return true after successfully casting the card


    }

    internal bool CardValid(Card cardBeingPlayed, Player usingOnplayer, Hand fromHand)
    {
        bool valid = false;
        if (cardBeingPlayed == null) // 
            return false; // Check if the card is null

        if (fromHand.isplayer)
        {
            if (cardBeingPlayed.cardData.cost <= player.mana) // Check if the card's cost is less than or equal to player's mana
            {
                if (usingOnplayer.isplayer && cardBeingPlayed.cardData.isDefenseCard)
                    valid = true;

                if (!usingOnplayer.isplayer && !cardBeingPlayed.cardData.isDefenseCard)
                    valid = true;
            }
        }

        else
        {
            if (cardBeingPlayed.cardData.cost <= enemy.mana) // Check if the card's cost is less than or equal to player's mana
            {
                if (!usingOnplayer.isplayer && cardBeingPlayed.cardData.isDefenseCard)
                    valid = true;

                if (usingOnplayer.isplayer && !cardBeingPlayed.cardData.isDefenseCard)
                    valid = true;
            }

        }
        return valid; // Return the validity of the card
    }

    internal void CastCard(Card card, Player usingonPlayer, Hand fromHand)

    {
        if (card.cardData.isMirrorCard)
        {
            usingonPlayer.SetMirror(true); // Set the mirror effect on the player
            isPlayable = true; // Set the game to playable after casting the mirror card
        }
        else
        {
            if (card.cardData.isDefenseCard)
            {
                usingonPlayer.health += card.cardData.damage; // Heal the player by the card's damage value

                if (usingonPlayer.health > usingonPlayer.maxHealth)
                    usingonPlayer.health = 5; // Cap the player's health at 5

                updateHealth(); // Update the health display in the game controller
                StartCoroutine(CastHealEffect(usingonPlayer)); // Start the heal effect coroutine
            }
            else // attack card
            {

            }

            // to do add score
            // to do update player mana 

        }
    }

    private System.Collections.IEnumerator CastHealEffect(Player usingOnPlayer)
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before casting the heal effect
        isPlayable = true; // Set the game to playable after casting the heal effect
    }
    internal void CastAttackEffect(Card card, Player usingOnPlayer)
    {
        GameObject effectGo = null;
        if (usingOnPlayer.isplayer)
            effectGo = Instantiate(effectFromRightPrefab, canvas.gameObject.transform);
        else
            effectGo = Instantiate(effectFromLeftPrefab, canvas.gameObject.transform);

        Effect effect = effectGo.GetComponent<Effect>();
        if (effect)
        {
            effect.targetPlayer = usingOnPlayer;
            effect.sourceCard = card;

            switch (card.cardData.damageType)
            {
                case CardData.Damagetype.Fire:
                    if (card.cardData.isMulti)
                        effect.effectImage.sprite = multifireballImage;
                    else
                        effect.effectImage.sprite = fireballImage;
                    break;
                case CardData.Damagetype.Ice:

                    if (card.cardData.isMulti)
                        effect.effectImage.sprite = multiIceballImage;
                    else
                        effect.effectImage.sprite = iceBallImage;

                    break;

                case CardData.Damagetype.Both:
                    if (card.cardData.isMulti)
                        effect.effectImage.sprite = FireAndIceImage;
                    else
                        effect.effectImage.sprite = FireAndIceImage;
                    break;
            }

        }

    }

    internal void updateHealth()
    {
        player.updateHealth(); // Update player's health
        enemy.updateHealth(); // Update enemy's health

        if (player.health <= 0)
        {
            // to do game over
        }

        if (enemy.health <= 0)
        {
            // to do new enemy
        }
    }
}
