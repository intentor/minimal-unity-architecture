using System;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// State to follow a given target using NavMeshAgent.
    /// </summary>
    public class FollowingState : State {
        private const float DestinationUpdateInterval = 0.5f;

        private readonly NavMeshAgent _agent;
        private readonly string _targetTag;

        private Transform _target;
        private float _lastDestinationUpdateTime;

        public FollowingState(NavMeshAgent agent, string targetTag) {
            _agent = agent != null ? agent : throw new ArgumentNullException(nameof(agent));
            _targetTag = !string.IsNullOrEmpty(targetTag) ? targetTag : throw new ArgumentNullException(nameof(targetTag));
        }

        public override void OnEnter() {
            var targetGameObject = GameObject.FindWithTag(_targetTag);
            _target = targetGameObject != null ? targetGameObject.transform : null;

            if (_target == null) {
                Debug.LogWarning($"No target found with tag '{_targetTag}' for FollowingState.");
                return;
            }

            _lastDestinationUpdateTime = 0f;
        }

        public override void Update() {
            if (_target == null) {
                return;
            }

            if (Time.time - _lastDestinationUpdateTime >= DestinationUpdateInterval) {
                _agent.SetDestination(_target.position);
                _lastDestinationUpdateTime = Time.time;
            }
        }

        public override void OnExit() {
            _agent.isStopped = true;
        }
    }
}
