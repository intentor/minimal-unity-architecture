using Modules.Shared.Scripts.Runtime.Events;
using Modules.Shared.Scripts.Runtime.StateManagement;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Game.Scripts.Runtime {
    /// <summary>
    /// Controls player behavior.
    /// </summary>
    [AddComponentMenu("Game/Player Controller")]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : StateMachine {
        [Header("Player Settings")]
        [SerializeField]
        [Tooltip("Player movement speed.")]
        [Min(0f)]
        private float _moveSpeed = 5f;

        [SerializeField]
        [Tooltip("Tag of enemy objects.")]
        [TagField]
        private string _enemyTag = "Enemy";

        [Header("Input Actions")]
        [SerializeField]
        [Tooltip("Input action for movement.")]
        private InputActionReference _actionMovement;

        [Header("Events Invoked")]
        [SerializeField]
        [Tooltip("Event invoked when the player is defeated.")]
        private GameEvent _onPlayerDefeated;

        private bool _collidedWithEnemy;

        protected override void SetupStates() {
            var rigidbody = GetComponent<Rigidbody>();
            var inputControlState = new InputControlState(_actionMovement, transform, rigidbody, _moveSpeed);
            var defeatState = new DefeatedState(gameObject, _onPlayerDefeated);

            AddState(inputControlState)
                .AddTransition(() => _collidedWithEnemy, defeatState);
            AddState(defeatState);
        }

        protected override void OnStateMachineStart() {
            _collidedWithEnemy = false;
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag(_enemyTag)) {
                _collidedWithEnemy = true;
            }
        }
    }
}