 using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDropHandler
{
    public UnityEngine.UI.Image playerImage = null;
    public UnityEngine.UI.Image mirrorImage = null;
    public UnityEngine.UI.Image healthNumberImage = null;
    public UnityEngine.UI.Image glowImage = null;

    // Implementation of IDropHandler interface
    public void OnDrop(PointerEventData eventData)
    {
        if (!GameController.instance.isPlayable) // Check if the game is playable
        {
            return; // Prevent dropping if the game is not playable
        }

        GameObject obj = eventData.pointerDrag;
        if (obj != null)
        {
            Card card = obj.GetComponent<Card>();
            if (card != null)
            {
                GameController.instance.UseCard(card, this, GameController.instance.playerHand); // Use the card
            }
        }
    }


    public int health = 5; // current health of the player
    public int maxHealth = 5; // maximum health of the player
    public int mana = 1;

    public bool isplayer;  // Indicates if this is the player's character
    public bool isFire; // Indicates if the player is using fire damage

    public GameObject[] manaballs = new GameObject[5]; // Array to hold mana balls

    private Animator animator;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        updateHealth(); // Update the health display at the start
    }
    internal void playHitAnim()
    {
        if (animator != null)

            animator.SetTrigger("Hit");

    }

    internal void updateHealth()
    {
        if (health >= 0 && health < GameController.instance.healthNumbers.Length)
        {
            healthNumberImage.sprite = GameController.instance.healthNumbers[health];
        }
        else
        {
            Debug.LogError("Health value out of range: " + health.ToString());
        }
    }

    internal void SetMirror(bool on)
    {
        mirrorImage.gameObject.SetActive(on); // Activate or deactivate the mirror image based on the parameter

    }

    internal bool hasMirror()
    {
        return mirrorImage.gameObject.activeInHierarchy; // Check if the mirror image is active
    }
}
