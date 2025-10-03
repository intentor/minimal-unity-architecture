using UnityEngine.Events;

namespace Modules.Shared.Scripts.Runtime.Pooling {
    /// <summary>
    /// Defines an object that can be despawned.
    /// </summary>
    public interface IDespawnable {
        /// <summary>
        /// Gets or sets the <see cref="UnityAction"/> responsible for despawning this object.
        /// </summary>
        UnityAction Despawn { get; set; }
    }
}