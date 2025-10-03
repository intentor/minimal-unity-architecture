using Modules.Shared.Scripts.Runtime.Events;
using TMPro;
using UnityEngine;

namespace Modules.Game.Scripts.Runtime {
    /// <summary>
    /// Manages score for the game.
    /// </summary>
    [AddComponentMenu("Game/Score Manager")]
    public class ScoreManager : MonoBehaviour {
        [SerializeField]
        [Tooltip("Score enearned per each defeat.")]
        private int _scorePerDefeat = 10;

        [SerializeField]
        [Tooltip("Text field for the score value.")]
        private TextMeshProUGUI _scoreText;

        [Header("Events Listened")]
        [SerializeField]
        [Tooltip("Event listened when an enemy is defeated.")]
        private GameEvent _triggerAddScore;

        private int _currentScore = 0;

        /// <summary>
        /// Adds score.
        /// </summary>
        public void AddScore() {
            _currentScore += _scorePerDefeat;
            UpdateScoreText();
        }

        private void Start() {
            UpdateScoreText();
        }

        private void OnEnable() {
            _triggerAddScore.Subscribe(AddScore);
        }

        private void OnDisable() {
            _triggerAddScore.Unsubscribe(AddScore);
        }

        private void UpdateScoreText() {
            _scoreText.text = _currentScore.ToString();
        }
    }
}