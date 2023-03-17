using UnityEngine;

namespace MercenariesProject
{
    //On start up, link a character to the closest tile.
    public class PositionOnGrid : MonoBehaviour
    {
        void Start()
        {
            var closestTile = GridManager.Instance.GetOverlayByTransform(transform.position);
            if (closestTile != null)
            {
                transform.position = closestTile.transform.position;

                //this should be more generic
                Hero hero = GetComponent<Hero>();

                if (hero != null)
                    hero.LinkCharacterToTile(closestTile);
            }
        }
    }
}
