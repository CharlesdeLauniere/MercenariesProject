using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class AttackRangePreviewOnHover : MonoBehaviour
    {
        public GameEventGameObject showAttackRange;

        public void DisplayRangePreview(BaseEventData eventData)
        {
            if (gameObject.GetComponent<Button>().IsInteractable())
                showAttackRange.Raise(null);
        }


        public void HideRangePreview(BaseEventData eventData)
        {
            if(gameObject.GetComponent<Button>().IsInteractable()) OverlayTileColorManager.Instance.ClearTiles(OverlayTileColorManager.Instance.AttackRangeColor);
        }
        
    }
}
