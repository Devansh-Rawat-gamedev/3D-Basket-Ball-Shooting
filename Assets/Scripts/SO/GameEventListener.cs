using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;
        public void OnEventRaised()
        {
            response.Invoke();
        }

        private void OnEnable()
        {
            if (gameEvent != null)
            {
                gameEvent.RegisterListener(this);
            }
        }
        private void OnDisable()
        {
            if (gameEvent != null)
            {
                gameEvent.UnregisterListener(this);
            }
        }
    }
