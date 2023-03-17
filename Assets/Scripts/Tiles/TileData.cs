using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MercenariesProject
{
    [CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData")]
    public class TileData : ScriptableObject
    {
        public List<GameObject> baseTile;
        public string message;
        public TileTypes type = TileTypes.Traversable;
        public ScriptableEffect effect;
        public List<Material> Tiles3D;
    }
}
