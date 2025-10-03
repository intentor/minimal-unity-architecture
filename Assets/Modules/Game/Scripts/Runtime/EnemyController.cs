using Modules.Shared.Scripts.Runtime.Events;
using Modules.Shared.Scripts.Runtime.Pooling;
using Modules.Shared.Scripts.Runtime.StateManagement;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Modules.Game.Scripts.Runtime {
    /// <summary>
    /// Controls enemy behavior.
    /// </summary>
    [AddComponentMenu("Game/Enemy Controller")]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : StateMachine, IDespawnable {
        public UnityAction Despawn { get; set; }

        [Header("Enemy Settings")]
        [SerializeField]
        [Tooltip("Tag of the object to follow.")]
        [TagField]
        private string _followTag = "Player";

        [SerializeField]
        [Tooltip("Tag of the area where enemies are defeated.")]
        [TagField]
        private string _defeatAreaTag = "DefeatArea";

        [Header("Events Invoked")]
        [SerializeField]
        [Tooltip("Event invoked when the enemy is defeated.")]
        private GameEvent _onEnemyDefeated;

        private bool _triggeredDefeatArea;

        protected override void SetupStates() {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var followingState = new FollowingState(navMeshAgent, _followTag);
            var defeatState = new DefeatedState(gameObject, _onEnemyDefeated);

            AddState(followingState)
                .AddTransition(() => _triggeredDefeatArea, defeatState);
            AddState(defeatState);
        }

        protected override void OnStateMachineStart() {
            _triggeredDefeatArea = false;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag(_defeatAreaTag)) {
                _triggeredDefeatArea = true;
            }
        }
    }
}