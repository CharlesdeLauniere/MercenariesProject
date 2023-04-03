using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MercenariesProject
{
    public class AttackRangePreviewOnHover : MonoBehaviour
    {
        public GameEventGameObject showAttackRange;
        public bool isAttackMode = false;
        //Displays a characters attack range on hover. null will default to character position. 
        public void DisplayRangePreview(BaseEventData eventData)
        {
            if (gameObject.GetComponent<Button>().IsInteractable())
                showAttackRange.Raise(null);
        }

        //Hides a characters attack range.
        public void HideRangePreview(BaseEventData eventData)
        {
            if(!isAttackMode) OverlayTileColorManager.Instance.ClearTiles(OverlayTileColorManager.Instance.AttackRangeColor);
        }
        public void SwapAttackMode()
        {
            isAttackMode = !isAttackMode;
        }
    }
}
