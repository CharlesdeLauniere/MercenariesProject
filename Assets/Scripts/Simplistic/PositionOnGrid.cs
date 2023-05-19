using UnityEngine;

namespace MercenariesProject
{

    public class PositionOnGrid : MonoBehaviour
    {
        void Start()
        {
            var closestTile = GridManager.Instance.GetOverlayByTransform(transform.position);
            if (closestTile != null)
            {
                transform.position = closestTile.transform.position;


                Hero hero = GetComponent<Hero>();

                if (hero != null)
                    hero.LinkCharacterToTile(closestTile);
            }
        }
    }
}
