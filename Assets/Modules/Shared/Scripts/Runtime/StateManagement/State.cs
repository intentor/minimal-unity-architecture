namespace Modules.Shared.Scripts.Runtime.StateManagement {
    /// <summary>
    /// Base class for a state.
    /// </summary>
    public abstract class State {
        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        public virtual void OnEnter() {
            // Not implemented.
        }

        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        public virtual void Update() {
            // Not implemented.
        }

        /// <summary>
        /// Called every fixed framerate frame while the state is active.
        /// </summary>
        public virtual void FixedUpdate() {
            // Not implemented.
        }

        /// <summary>
        /// Called when the state is exited.
        /// </summary>
        public virtual void OnExit() {
            // Not implemented.
        }

        /// <summary>
        /// Cleans up resources used by the state.
        /// </summary>
        public virtual void Dispose() {
            // Not implemented.
        }
    }
}
