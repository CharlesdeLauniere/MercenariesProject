using System;
using UnityEngine;

namespace MercenariesProject
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventGameObject", menuName = "GameEvents/GameEventGameObject", order = 2)]
    public class GameEventGameObject : GameEvent<GameObject>
    {
        public GameObject gameObject;
    }
}
