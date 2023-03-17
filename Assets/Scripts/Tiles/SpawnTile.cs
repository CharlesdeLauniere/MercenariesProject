using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MercenariesProject
{
    //A tile object that is used for spawning characters on a specific location.
    public class SpawnTile : MonoBehaviour
    {
        public Vector2Int gridLocation;
        //public Vector2Int grid2DLocation { get { return new Vector2Int(gridLocation.x, gridLocation.y); } }

        private void Update()
        {
            if (gridLocation == new Vector2Int(0, 0))
                GetGridLocation();
        }

        public void GetGridLocation()
        {
            Vector2 tilePositionPos2d = new Vector2(transform.position.x, transform.position.z);

            RaycastHit2D[] hits = Physics2D.RaycastAll(tilePositionPos2d, Vector2.zero);

            if (hits.Length > 0)
            {
                hits.OrderByDescending(i => i.collider.transform.position.y).First();

                if (hits[0].collider.gameObject.GetComponent<Tile>() != null)
                {
                    var tile = hits[0].collider.gameObject.GetComponent<Tile>();
                    gridLocation = tile.gridLocation;
                }
            }
        }
    }
}
