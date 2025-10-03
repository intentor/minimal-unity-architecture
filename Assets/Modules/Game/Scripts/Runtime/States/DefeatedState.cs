using System;
using Modules.Shared.Scripts.Runtime.Events;
using Modules.Shared.Scripts.Runtime.Pooling;
using UnityEngine;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// State when a character is defeated.
    /// </summary>
    public class DefeatedState : State {
        private readonly GameObject _parent;
        private readonly GameEvent _onDefeated;

        public DefeatedState(GameObject parent, GameEvent onDefeated) {
            _parent = parent != null ? parent : throw new ArgumentNullException(nameof(parent));
            _onDefeated = onDefeated != null ? onDefeated : throw new ArgumentNullException(nameof(onDefeated));
        }

        public override void OnEnter() {
            if (_parent.TryGetComponent<IDespawnable>(out var despawnable)) {
                despawnable.Despawn();
            }

            _onDefeated?.Invoke();
        }
    }
}
