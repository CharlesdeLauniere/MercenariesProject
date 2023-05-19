using UnityEngine;

namespace MercenariesProject
{
    //Liste de types de tuiles
    [CreateAssetMenu(fileName = "TileList", menuName = "ScriptableObjects/TileList")]
    public class TileDataRuntimeSet : RuntimeSet<TileData> { };
}