using System;
using UnityEngine;

namespace MercenariesProject
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventCommand", menuName = "GameEvents/GameEventCommand", order = 3)]
    public class GameEventCommand : GameEvent<EventCommand>
    {
        public EventCommand value;
    }
}
