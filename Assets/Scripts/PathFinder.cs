using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MercenariesProject
{
   

    public class PathFinder
    {
        public List<Tile> FindPath(Tile start, Tile end)
        {
            //Debug.Log("Find Pathing");
            List<Tile> openList = new();
            List<Tile> closedList = new();

            openList.Add(start);

            while (openList.Count > 0)
            {
                Tile currentTile = openList.OrderBy(x => x.F).First();

                openList.Remove(currentTile);
                closedList.Add(currentTile);

                if (currentTile == end)
                {
                    //finalize path
                    return GetFinishedList(start, end);
                }

                //var neighbourTiles = GetNeighbourTiles(currentTile);

                foreach (var tile in GetNeighbourTiles(currentTile))
                {
                    if (!(tile.isWalkable) || closedList.Contains(tile) || tile.OccupiedUnit == true)
                    {
                        continue;
                    }
                    tile.G = GetManhattanDistance(start, tile);
                    tile.H = GetManhattanDistance(end, tile);

                    tile.previous = currentTile;

                    if (!openList.Contains(tile))
                    {
                        openList.Add(tile);
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

        // Block movement
        private int GetManhattanDistance(Tile start, Tile neighbour)
        {
            return Mathf.Abs(start.gridLocation.x - neighbour.gridLocation.x) + Math.Abs(start.gridLocation.y - neighbour.gridLocation.y);
        }

        private List<Tile> GetNeighbourTiles(Tile currentTile)
        {
            var grid = GridManager.Instance.tileMap;

            List<Tile> neighbours = new List<Tile>();

            //top neighbour
            Vector2Int locationToCheck = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y + 1);
            if (grid.ContainsKey(locationToCheck))
            {
                neighbours.Add(grid[locationToCheck]);
            }
            //bottom neighbour
            locationToCheck = new Vector2Int(currentTile.gridLocation.x, currentTile.gridLocation.y - 1);
            if (grid.ContainsKey(locationToCheck))
            {
                neighbours.Add(grid[locationToCheck]);
            }
            //right neighbour
            locationToCheck = new Vector2Int(currentTile.gridLocation.x + 1, currentTile.gridLocation.y);
            if (grid.ContainsKey(locationToCheck))
            {
                neighbours.Add(grid[locationToCheck]);
            }
            //left neighbour
            locationToCheck = new Vector2Int(currentTile.gridLocation.x - 1, currentTile.gridLocation.y);
            if (grid.ContainsKey(locationToCheck))
            {
                neighbours.Add(grid[locationToCheck]);
            }
            return neighbours;
        }
    }

}