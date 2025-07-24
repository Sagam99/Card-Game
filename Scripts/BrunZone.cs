using UnityEngine;
using UnityEngine.EventSystems;

public class BrunZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        Card card = obj.GetComponent<Card>();
        if (card != null)
        {
            GameController.instance.playerHand.RemoveCard(card);

        }
        
    }
}
