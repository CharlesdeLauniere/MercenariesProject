using System;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventGameObjectList", menuName = "GameEvents/GameEventGameObjectList", order = 3)]
    public class GameEventGameObjectList : GameEvent<List<GameObject>>
    {
        public List<GameObject> gameObject;
    }
}
