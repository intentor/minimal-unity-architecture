using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Shared.Scripts.Runtime.Events {
    /// <summary>
    /// Represents a game event that can be used for event-driven architecture.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Event")]
    public class GameEvent : ScriptableObject {
#if UNITY_EDITOR
        [TextArea]
        [Tooltip("Description of the event.")]
        public string Description;
#endif

        private readonly List<UnityAction> _listeners = new();

        /// <summary>
        /// Subscribes a listener to the event.
        /// </summary>
        /// <param name="listener">Listener to subscribe.</param>
        public void Subscribe(UnityAction listener) {
            _listeners.Add(listener);
        }

        /// <summary>
        /// Unsubscribes a listener from the event.
        /// </summary>
        /// <param name="listener">Listener to unsubscribe.</param>
        public void Unsubscribe(UnityAction listener) {
            _listeners.Remove(listener);
        }

        /// <summary>
        /// Invokes the event, notifying all subscribed listeners.
        /// </summary>
        public void Invoke() {
            for (var i = 0; i < _listeners.Count; i++) {
                _listeners[i].Invoke();
            }
        }
    }
}