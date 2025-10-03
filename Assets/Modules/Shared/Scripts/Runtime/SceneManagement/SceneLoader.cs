using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Shared.Scripts.Runtime.SceneManagement {
    /// <summary>
    /// Loades scenes within the game.
    /// </summary>
    [AddComponentMenu("Shared/Scene Loader")]
    public class SceneLoader : MonoBehaviour {
        /// <summary>
        /// Loads a scene by its name.
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
