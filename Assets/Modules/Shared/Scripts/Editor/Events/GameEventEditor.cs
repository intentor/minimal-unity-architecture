using UnityEngine;
using UnityEditor;
using Modules.Shared.Scripts.Runtime.Events;

namespace Modules.Shared.Scripts.Editor.Events {
    /// <summary>
    /// Custom editor for <see cref="GameEvent"/>.
    /// </summary>
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (Application.isPlaying) {
                EditorGUILayout.Space();
                if (GUILayout.Button(new GUIContent("Invoke", "Invoke this event."))) {
                    ((GameEvent)target).Invoke();
                }
            }

        }
    }
}