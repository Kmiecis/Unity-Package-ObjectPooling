using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// Unity specific <see cref="IMultiPool{T}"/> implementation
    /// </summary>
    [Serializable]
    public class UnityMultiPool<T> : IMultiPool<T>
        where T : UnityEngine.Object
    {
        [SerializeField]
        protected int _capacity = 1;
        [SerializeField]
        protected T[] _prefabs;
        [SerializeField]
        protected Transform _parent;

        protected readonly Dictionary<Type, Queue<T>> _pools;

        protected int _constructed = 0;

        public int Count
        {
            get => _constructed;
        }

        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public UnityMultiPool()
        {
            _pools = new Dictionary<Type, Queue<T>>();
        }

        public void Prewarm(int count)
        {
            var construct = Mathf.Min(count, _capacity);
            foreach (var prefab in _prefabs)
            {
                for (int i = 0; i < construct; ++i)
                {
                    _constructed += 1;
                    var constructed = Construct(prefab);
                    Return(constructed);
                }
            }
        }

        public virtual U Borrow<U>()
            where U : T
        {
            return WrappedBorrow<U>();
        }

        public virtual void Return<U>(U item)
            where U : T
        {
            WrappedReturn(item);
        }

        public virtual void Clear()
        {
            foreach (var entry in _pools)
            {
                var pool = entry.Value;

                while (pool.TryDequeue(out var item))
                {
                    WrappedDestroy(item);
                }
            }
        }

        public int GetActiveCount()
        {
            return _constructed - GetInactiveCount();
        }

        public int GetInactiveCount()
        {
            var result = 0;

            foreach (var entry in _pools)
            {
                var pool = entry.Value;
                result += pool.Count;
            }

            return result;
        }

        public void Dispose()
        {
            Clear();
        }

        protected U WrappedBorrow<U>()
            where U : T
        {
            var pool = GetOrCreatePool<U>();

            if (pool.Count == 0)
            {
                return WrappedConstruct<U>();
            }

            return (U)pool.Dequeue();
        }

        protected void WrappedReturn<U>(U item)
            where U : T
        {
            var pool = GetOrCreatePool<U>();

            if (pool.Count >= _capacity)
            {
                WrappedDestroy(item);
            }
            else
            {
                pool.Enqueue(item);
            }
        }

        protected U WrappedConstruct<U>()
            where U : T
        {
            if (TryGetPrefab<U>(out var prefab))
            {
                _constructed += 1;
                return Construct(prefab);
            }

            return null;
        }

        protected void WrappedDestroy<U>(U item)
            where U : T
        {
            _constructed -= 1;

            Destroy(item);
        }

        protected virtual U Construct<U>(U prefab)
            where U : T
        {
            return UnityEngine.Object.Instantiate(prefab, _parent, false);
        }

        protected virtual void Destroy<U>(U item)
            where U : T
        {
            UnityEngine.Object.Destroy(item);
        }

        protected bool TryGetPrefab<U>(out U prefab)
            where U : T
        {
            foreach (var item in _prefabs)
            {
                if (item is U casted)
                {
                    prefab = casted;
                    return true;
                }
            }

            prefab = null;
            return false;
        }

        protected Queue<T> GetOrCreatePool<U>()
            where U : T
        {
            return GetOrCreatePool(typeof(U));
        }

        protected Queue<T> GetOrCreatePool(Type type)
        {
            if (!_pools.TryGetValue(type, out var result))
            {
                _pools[type] = result = new Queue<T>();
            }

            return result;
        }
    }
}