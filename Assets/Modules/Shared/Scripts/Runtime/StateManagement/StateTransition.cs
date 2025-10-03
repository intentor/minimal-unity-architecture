using System;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// Represents a transition between states in a state machine.
    /// </summary>
    public class StateTransition {
        public Func<bool> Predicate { get; private set; }
        public State TargetState { get; private set; }

        public StateTransition(Func<bool> predicate, State targetState) {
            Predicate = predicate;
            TargetState = targetState;
        }
    }
}