using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MercenariesProject
{
   
    public class MovementRangePreviewOnHover : MonoBehaviour
    {
        public GameEventGameObject showMovementRange;

        //Displays a characters move range on hover. null will default to character position. 
        public void DisplayRangePreview(BaseEventData eventData)
        {
            if (gameObject.GetComponent<Button>().IsInteractable())
                showMovementRange.Raise(null);
        }

        //Hides a characters move range.
        public void HideRangePreview(BaseEventData eventData)
        {
            if (gameObject.GetComponent<Button>().IsInteractable()) OverlayTileColorManager.Instance.ClearTiles(OverlayTileColorManager.Instance.MoveRangeColor);
        }
    }
}
