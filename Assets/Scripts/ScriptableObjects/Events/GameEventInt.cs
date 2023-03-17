using System;
using UnityEngine;

namespace MercenariesProject
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventInt", menuName = "GameEvents/GameEventInt", order = 3)]
    public class GameEventInt : GameEvent<int>
    {
        public int value;
    }
}
