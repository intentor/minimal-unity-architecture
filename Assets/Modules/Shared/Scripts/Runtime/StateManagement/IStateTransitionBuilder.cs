using System;

namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// Builds state transitions.
    /// </summary>
    public interface IStateTransitionBuilder {
        /// <summary>
        /// Adds a transition from the current state to the target state based on the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate function to determine if the transition should occur.</param>
        /// <param name="targetState">The state to transition to.</param>
        /// <returns>Current instance for chaining.</returns>
        IStateTransitionBuilder AddTransition(Func<bool> predicate, State targetState);
    }
}
