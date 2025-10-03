using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// State when a character is being controlled by input actions.
    /// </summary>
    public class InputControlState : State {
        private readonly InputActionReference _actionMovement;
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly float _moveSpeed;

        private Vector2 _currentInput;

        public InputControlState(InputActionReference actionMovement,
            Transform transform, Rigidbody rigidbody, float moveSpeed) {
            _actionMovement = actionMovement;
            _transform = transform;
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;
        }

        public override void OnEnter() {
            _actionMovement.action.performed += OnMovementPerformed;
            _actionMovement.action.canceled += OnMovementCanceled;
        }

        public override void Update() {
            if (_currentInput == Vector2.zero) {
                _rigidbody.linearVelocity = Vector3.zero;
                return;
            }

            // Convert 2D input to 3D movement.
            var movement = new Vector3(_currentInput.x, 0f, _currentInput.y) * _moveSpeed;

            // Apply velocity to rigidbody.
            _rigidbody.linearVelocity = new Vector3(movement.x, _rigidbody.linearVelocity.y, movement.z);

            // Make the transform point in the direction of movement.
            if (movement.magnitude > 0.01f) {
                _transform.rotation = Quaternion.LookRotation(movement.normalized);
            }
        }

        public override void OnExit() {
            _actionMovement.action.performed -= OnMovementPerformed;
            _actionMovement.action.canceled -= OnMovementCanceled;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context) {
            _currentInput = context.ReadValue<Vector2>();
        }

        private void OnMovementCanceled(InputAction.CallbackContext context) {
            _currentInput = Vector2.zero;
        }
    }
}
