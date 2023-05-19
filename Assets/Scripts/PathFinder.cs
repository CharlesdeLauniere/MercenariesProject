using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MercenariesProject
{


    public class PathFinder
    {

        public List<Tile> FindPath(Tile start, Tile end, List<Tile> searchableTiles, bool ignoreObstacles = false, bool walkTroughAllies = true)
        {
            List<Tile> openList = new List<Tile>();
            List<Tile> closedList = new List<Tile>();

            openList.Add(start);

            while (openList.Count > 0)
            {
                Tile currentTile = openList.OrderBy(x => x.F).First();

                openList.Remove(currentTile);
                closedList.Add(currentTile);

                if (currentTile == end)
                {
                    //finalize our path. 
                    return GetFinishedList(start, end);
                }

                var neighbourTiles = GridManager.Instance.GetNeighbourTiles(currentTile, searchableTiles, ignoreObstacles, walkTroughAllies);

                foreach (var neighbour in neighbourTiles)
                {
                    if (closedList.Contains(neighbour))
                    {
                        continue;
                    }

                    neighbour.G = GetManhattanDistance(start, neighbour);
                    neighbour.H = GetManhattanDistance(end, neighbour);

                    neighbour.previous = currentTile;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }

            return new List<Tile>();
        }
        private List<Tile> GetFinishedList(Tile start, Tile end)
        {
            List<Tile> finishedList = new();
            Tile currentTile = end;

            while (currentTile != start)
            {
                finishedList.Add(currentTile);
                currentTile = currentTile.previous;
            }

            finishedList.Reverse();

            return finishedList;
        }

        private int GetManhattanDistance(Tile start, Tile neighbour)
        {
            return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Math.Abs(start.gridLocation.y - neighbour.gridLocation.y);
        }


    }
}