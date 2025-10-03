using System;
using System.Collections.Generic;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// Node representing a state in a state machine.
    /// </summary>
    public class StateNode : IStateTransitionBuilder {
        /// <summary>
        /// The state represented by this node.
        /// </summary>
        public State State { get; private set; }

        /// <summary>
        /// Transitions from this state to other states.
        /// </summary>
        public List<StateTransition> Transitions { get; private set; } = new();

        public StateNode(State state) {
            State = state;
        }

        public IStateTransitionBuilder AddTransition(Func<bool> predicate, State targetState) {
            Transitions.Add(new(predicate, targetState));
            return this;
        }
    }
}
