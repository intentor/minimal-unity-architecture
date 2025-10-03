using System.Collections;
using System.Collections.Generic;
using Modules.Shared.Scripts.Runtime.Events;
using UnityEngine;

namespace Modules.Shared.Scripts.Runtime.Pooling {
    /// <summary>
    /// Spawns objects within the game.
    /// </summary>
    [AddComponentMenu("Shared/Object Spawner")]
    public class ObjectSpawner : MonoBehaviour {
        [SerializeField]
        [Tooltip("The prefabs to spawn.")]
        private GameObject[] _prefabs;

        [SerializeField]
        [Tooltip("The positions to spawn the objects.")]
        private Transform[] _positions;

        [Header("Initial Spawn")]
        [SerializeField]
        [Tooltip("The number of objects to spawn initially.")]
        private int _initialSpawnCount = 3;

        [SerializeField]
        [Tooltip("The interval between spawns.")]
        [Min(0.1f)]
        private float initialSpawnInterval = 3f;

        [Header("Events Listened")]
        [SerializeField]
        [Tooltip("The event to trigger when a new object is spawned.")]
        private GameEvent _triggerSpawn;

        private readonly Dictionary<GameObject, SpawnPool> _pools = new();
        private WaitForSeconds _spawnIntervalWait;
        private Coroutine _spawnRoutine;
        private int _nextPositionIndex = 0;
        private int _currentSpawnedCount = 0;

        private void Awake() {
            _spawnIntervalWait = new WaitForSeconds(initialSpawnInterval);

            for (var i = 0; i < _prefabs.Length; i++) {
                var prefab = _prefabs[i];
                if (prefab == null) {
                    continue;
                }

                _pools.Add(prefab, new SpawnPool(prefab, transform, _initialSpawnCount));
            }
        }

        private void OnEnable() {
            _triggerSpawn.Subscribe(Spawn);
            _spawnRoutine = StartCoroutine(InitialSpawn());
        }

        private void OnDisable() {
            _triggerSpawn.Unsubscribe(Spawn);

            if (_spawnRoutine != null) {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        private IEnumerator InitialSpawn() {
            while (true) {
                yield return _spawnIntervalWait;

                if (_currentSpawnedCount < _initialSpawnCount) {
                    Spawn();
                    _currentSpawnedCount++;
                } else {
                    break;
                }
            }

            _spawnRoutine = null;
        }

        private void Spawn() {
            if (_positions.Length == 0 || _pools.Count == 0) {
                return;
            }

            var spawnedObject = SpawnObject();

            var position = GetNextPosition();
            spawnedObject.transform.position = position;
        }

        private Vector3 GetNextPosition() {
            if (++_nextPositionIndex >= _positions.Length) {
                _nextPositionIndex = 0;
            }

            return _positions[_nextPositionIndex].position;
        }

        private GameObject SpawnObject() {
            var prefab = _prefabs[Random.Range(0, _prefabs.Length)];
            var pool = _pools[prefab];
            return pool.Get();
        }
    }
}