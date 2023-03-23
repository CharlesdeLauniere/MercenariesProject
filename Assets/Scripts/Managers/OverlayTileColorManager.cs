using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    //Handles the colouring of tiles. 
    public class OverlayTileColorManager : MonoBehaviour
    {
        private static OverlayTileColorManager _instance;
        public static OverlayTileColorManager Instance { get { return _instance; } }

        public Dictionary<Color, List<Tile>> coloredTiles;
        
            [Header("Overlay Colors")]
        public Color MoveRangeColor;
        public Color AttackRangeColor;
        public Color BlockedTileColor;

        public enum TileColors
        {
            MovementColor,
            AttackRangeColor,
            AttackColor
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            coloredTiles = new Dictionary<Color, List<Tile>>();
           
        }

        //Remove colours from all tiles. 
        public void ClearTiles(Color? color = null)
        {
            if (color.HasValue)
            {
                if (coloredTiles.ContainsKey(color.Value))
                {
                    var tiles = coloredTiles[color.Value];
                    coloredTiles.Remove(color.Value);
                    foreach (var coloredTile in tiles)
                    {
                        coloredTile.HideTile();

                        foreach (var usedColors in coloredTiles.Keys)
                        {
                            foreach (var usedTile in coloredTiles[usedColors])
                            {
                                if (coloredTile.gridLocation == usedTile.gridLocation)
                                {
                                    coloredTile.ShowTile(usedColors);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var item in coloredTiles.Keys)
                {
                    foreach (var colouredTile in coloredTiles[item])
                    {
                        colouredTile.HideTile();
                    }
                }

                coloredTiles.Clear();
            }
        }

        //Color tiles to specific color
        public void ColorTiles(Color color, List<Tile> Tiles)
        {
            ClearTiles(color);
            foreach (var tile in Tiles)
            {
                tile.ShowTile(color);

                if (!tile.isWalkable)
                    tile.ShowTile(BlockedTileColor);
            }

            coloredTiles.Add(color, Tiles);
        }

        //Color only one tile. 
        public void ColorSingleTile(Color color, Tile tile)
        {
            ClearTiles(color);
            tile.ShowTile(color);

            if (!tile.isWalkable)
                tile.ShowTile(BlockedTileColor);


            var list = new List<Tile>();
            list.Add(tile);
            coloredTiles.Add(color, list);
        }
    }
}
