using System;
using System.Collections.Generic;
using UnityEngine;


namespace MercenariesProject
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventStringList", menuName = "GameEvents/GameEventStringList", order = 2)]
    public class GameEventStringList : GameEvent<List<string>>
    {
        public List<string> _stringList;
    }
}
