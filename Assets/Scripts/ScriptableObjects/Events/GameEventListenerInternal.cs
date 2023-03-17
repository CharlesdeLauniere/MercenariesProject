
using UnityEngine;
using UnityEngine.Events;

namespace MercenariesProject
{
    public class GameEventListenerInternal
    {
        private GameEvent gameEvent;
        private UnityEvent response;

        public void RegisterEvent(GameEvent gameEvent, UnityEvent response)
        {
            if (gameEvent != null)
            {
                this.gameEvent = gameEvent;
                this.response = response;

                gameEvent.RegisterListener(this);
            }
        }

        public void UnregisterEvent()
        {
            if (gameEvent != null)
            {
                gameEvent.UnregisterListener(this);
            }
        }

        public void OnEventRaised()
        {
            if (response != null)
            {
                if (gameEvent.name != "CorsorEvent" && gameEvent.name != "FocusedOnNewTile")
                    Debug.Log("Received: " + gameEvent.name + ". \r\n Calling: " + response.GetPersistentTarget(0) + " -> " + response.GetPersistentMethodName(0));

                response.Invoke();
            }
        }
    }

    public class GameEventListenerInternal<T>
    {
        private GameEvent<T> gameEvent;
        private UnityEvent<T> response;

        public void RegisterEvent(GameEvent<T> gameEvent, UnityEvent<T> response)
        {
            if (gameEvent != null)
            {
                this.gameEvent = gameEvent;
                this.response = response;

                gameEvent.RegisterListener(this);
            }
        }

        public void UnregisterEvent()
        {
            if (gameEvent != null)
            {
                gameEvent.UnregisterListener(this);
            }
        }

        public void OnEventRaised(T param)
        {
            if (response != null)
            {
                if (gameEvent.name != "FocusOnTile")
                    Debug.Log("Received: " + gameEvent.name + ". \r\n Calling: " + response.GetPersistentTarget(0) + " -> " + response.GetPersistentMethodName(0));

                response.Invoke(param);
            }
        }
    }
}