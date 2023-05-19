using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MercenariesProject
{
   
    public class MovementRangePreviewOnHover : MonoBehaviour
    {
        public GameEventGameObject showMovementRange;


        public void DisplayRangePreview(BaseEventData eventData)
        {
            if (gameObject.GetComponent<Button>().IsInteractable())
                showMovementRange.Raise(null);
        }


        public void HideRangePreview(BaseEventData eventData)
        {
            if (gameObject.GetComponent<Button>().IsInteractable()) OverlayTileColorManager.Instance.ClearTiles(OverlayTileColorManager.Instance.MoveRangeColor);
        }
    }
}
