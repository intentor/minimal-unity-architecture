using UnityEngine;
using UnityEngine.Pool;

namespace Modules.Shared.Scripts.Runtime.Pooling {
    /// <summary>
    /// Spawns objects within the game.
    /// </summary>
    [AddComponentMenu("Shared/Object Spawner")]
    public class SpawnPool {
        private readonly ObjectPool<GameObject> _pool;
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private bool _preWarming;

        /// <summary>
        /// Prefab related to the pool.
        /// </summary>
        public GameObject Prefab => _prefab;

        public SpawnPool(GameObject prefab, Transform parent, int initialSize, int MaxPoolSize = 10) {
            _prefab = prefab;
            _parent = parent;

            _pool = new ObjectPool<GameObject>(
                CreatePoolItem,
                OnGetPoolItem,
                OnReleasePoolItem,
                OnDestroyPoolItem,
                true,
                initialSize,
                MaxPoolSize
            );

            PreWarm(initialSize);
        }

        /// <summary>
        /// Gets an object from the pool.
        /// </summary>
        /// <returns>Object from the pool.</returns>
        public GameObject Get() {
            return _pool.Get();
        }

        private void PreWarm(int quantity) {
            _preWarming = true;
            var warmedItems = new GameObject[quantity];

            for (var i = 0; i < quantity; i++) {
                warmedItems[i] = _pool.Get();
            }

            for (var i = 0; i < quantity; i++) {
                _pool.Release(warmedItems[i]);
            }

            _preWarming = false;
        }

        private GameObject CreatePoolItem() {
            var item = Object.Instantiate(Prefab);
            item.SetActive(false);
            item.transform.SetParent(_parent);

            if (item.TryGetComponent<IDespawnable>(out var despawnable)) {
                despawnable.Despawn = () => _pool.Release(item);
            }

            return item;
        }

        private void OnGetPoolItem(GameObject item) {
            if (_preWarming) {
                return;
            }

            item.SetActive(true);
        }

        private void OnReleasePoolItem(GameObject item) {
            if (_preWarming) {
                return;
            }

            item.SetActive(false);
            item.transform.SetParent(_parent);
        }

        private void OnDestroyPoolItem(GameObject item) {
            Object.Destroy(item);
        }
    }
}