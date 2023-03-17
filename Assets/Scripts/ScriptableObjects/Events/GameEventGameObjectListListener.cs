using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MercenariesProject
{
    public class GameEventGameObjectListListener : GameEventListener<List<GameObject>>
    {
        [SerializeField] private GameEventGameObjectList eventGameObject = null;
        [SerializeField] private UnityEvent<List<GameObject>> response = null;

        public override GameEvent<List<GameObject>> Event => eventGameObject;
        public override UnityEvent<List<GameObject>> Response => response;
    }
}
