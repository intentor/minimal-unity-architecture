using System.Collections.Generic;
using UnityEngine;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// Base class for state machines.
    /// </summary>
    public abstract class StateMachine : MonoBehaviour {
        private readonly List<StateNode> _nodes = new();
        private State _initialState;
        private StateNode _current;

        private void Awake() {
            SetupStates();
        }

        private void OnEnable() {
            SetState(_initialState);
            OnStateMachineStart();
        }

        private void Update() {
            var transition = GetTransition();
            if (transition != null) {
                SetState(transition.TargetState);
            }

            _current?.State.Update();
        }

        private void FixedUpdate() {
            _current?.State.FixedUpdate();
        }

        private void OnDestroy() {
            _current?.State.Dispose();
        }

        protected IStateTransitionBuilder AddState(State state) {
            _initialState ??= state ?? throw new System.ArgumentNullException(nameof(state));

            var node = new StateNode(state);
            _nodes.Add(node);
            return node;
        }

        private void SetState(State state) {
            if (_current?.State == state) {
                return;
            }

            _current?.State.OnExit();

            _current = null;
            for (int i = 0; i < _nodes.Count; i++) {
                if (_nodes[i].State == state) {
                    _current = _nodes[i];
                    _current.State.OnEnter();
                    break;
                }
            }
        }

        protected abstract void SetupStates();

        protected virtual void OnStateMachineStart() {
            // Optional override for additional setup when the state machine starts.
        }

        protected virtual void OnStart() {
            // Optional override for additional setup after states are configured.
        }

        private StateTransition GetTransition() {
            if (_current == null) {
                return null;
            }

            for (int i = 0; i < _current.Transitions.Count; i++) {
                var transition = _current.Transitions[i];
                if (transition.Predicate()) {
                    return transition;
                }
            }

            return null;
        }
    }
}
