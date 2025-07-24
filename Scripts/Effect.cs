using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{

    public Player targetPlayer = null; // Reference to the target player
    public Card sourceCard = null; // Reference to the source card that triggered the effect
    public UnityEngine.UI.Image effectImage = null; // Image to display the effect

    public void EndTrigger()
    {
        int damage = sourceCard.cardData.damage; // Get the damage value from the card data
        if (!targetPlayer.isplayer) // monster // enemy
        {
            if (sourceCard.cardData.damageType == CardData.Damagetype.Fire && targetPlayer.isFire)
                damage = damage / 2;
            if (sourceCard.cardData.damageType == CardData.Damagetype.Ice && targetPlayer.isFire)
                damage = damage / 2;

        }
        targetPlayer.health -= damage; // Apply damage to the target player's health
        
        GameController.instance.updateHealth(); // Update the health display in the game controller
                                       //to do check for death

        GameController.instance.isPlayable = true; // Set the game to playable after applying the effect


        Destroy(gameObject); // Destroy the effect object after applying damage
        

    }   
    




}
