using UnityEngine;
using UnityEngine.Events;

namespace Modules.Shared.Scripts.Runtime.Events {
    /// <summary>
    /// Trigger a UnityEvent when a GameEvent is invoked.
    /// </summary>
    [AddComponentMenu("Shared/Game Event Trigger")]
    public class GameEventTrigger : MonoBehaviour {
        [SerializeField]
        [Tooltip("Event to trigger.")]
        private GameEvent _triggerEvent;

        [Space]
        [SerializeField]
        [Tooltip("Event invoked when the script starts.")]
        private UnityEvent OnStart;

        [SerializeField]
        [Tooltip("Event invoked when the game event is triggered.")]
        private UnityEvent OnEventTrigger;

        private void Start() {
            OnStart?.Invoke();
        }

        private void OnEnable() {
            _triggerEvent?.Subscribe(OnEventTriggered);
        }

        private void OnDisable() {
            _triggerEvent?.Unsubscribe(OnEventTriggered);
        }

        private void OnEventTriggered() {
            OnEventTrigger?.Invoke();
        }
    }
}
