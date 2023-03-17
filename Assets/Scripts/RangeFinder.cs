using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MercenariesProject
{


    public class RangeFinder
    {
        //Gets all tiles within a range
        public List<Tile> GetTilesInRange(Tile startingTile, int range, bool ignoreObstacles = false, bool walkThroughAllies = true)
        {
            var inRangeTiles = new List<Tile>();
            int stepCount = 0;

            inRangeTiles.Add(startingTile);

            var tileForPreviousStep = new List<Tile>();
            tileForPreviousStep.Add(startingTile);

            while (stepCount < range)
            {
                var surroundingTiles = new List<Tile>();

                foreach (var item in tileForPreviousStep)
                {
                    surroundingTiles.AddRange(GridManager.Instance.GetNeighbourTiles(item, new List<Tile>(), ignoreObstacles, walkThroughAllies));
                }

                inRangeTiles.AddRange(surroundingTiles);
                tileForPreviousStep = surroundingTiles;
                stepCount++;
            }

            return inRangeTiles.Distinct().ToList();
        }
        
    }
}


