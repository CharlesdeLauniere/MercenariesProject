using UnityEngine;

namespace MercenariesProject
{
    //Not very useful. List of tile types the map has. 
    [CreateAssetMenu(fileName = "TileList", menuName = "ScriptableObjects/TileList")]
    public class TileDataRuntimeSet : RuntimeSet<TileData> { };
}