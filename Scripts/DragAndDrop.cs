using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    private  Vector3 originalPosition; // Store the original position of the object
    public void OnBeginDrag(PointerEventData eventData)
    {
         originalPosition = transform.position;
    CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
    if (canvasGroup == null)
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    canvasGroup.blocksRaycasts = false; // Disable raycasts to allow dropping
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!GameController.instance.isPlayable) // Check if the game is playable
        {
            return; // Prevent dragging if the game is not playable
        }
        transform.position += (Vector3)eventData.delta;
        Card card = GetComponent<Card>(); // Get the Card component from the dragged object

        foreach (GameObject hover in eventData.hovered) // Check all hovered objects
        {
            BrunZone burnZone = hover.GetComponent<BrunZone>();  // Check if the hovered object is a burn zone
            if (burnZone != null)

            {
                card.burnImage.gameObject.SetActive(true); // Show burn image when hovering over burn zone
            }
            else
            {
                card.burnImage.gameObject.SetActive(false); // Hide burn image when not hovering over burn zone
            }
        }
	}

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPosition;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // Enable raycasts to allow dropping
        }
    
	}
}
