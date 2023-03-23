using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MercenariesProject
{
    public class GameEventStringListListener : GameEventListener<List<string>>
    {
        [SerializeField] private GameEventStringList eventGameObject = null;
        [SerializeField] private UnityEvent<List<string>> response = null;

        public override GameEvent<List<string>> Event => eventGameObject;
        public override UnityEvent<List<string>> Response => response;
    }
}
