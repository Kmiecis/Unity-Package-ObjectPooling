using System.Collections.Generic;
using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// Unity specific <see cref="IPool{T}"/> implementation
    /// </summary>
    [System.Serializable]
    public class UnityPool<T> : IPool<T>
        where T : Object
    {
        [SerializeField]
        protected int _capacity = 1;
        [SerializeField]
        protected T _prefab;
        [SerializeField]
        protected Transform _parent;

        protected readonly Queue<T> _pool;

        protected int _constructed;

        public int Count
        {
            get => _constructed;
        }

        public int ActiveCount
        {
            get => _constructed - _pool.Count;
        }

        public int InactiveCount
        {
            get => _pool.Count;
        }

        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public UnityPool()
        {
            _pool = new Queue<T>();
        }

        public void Prewarm(int count)
        {
            var construct = Mathf.Min(count, _capacity);
            for (int i = 0; i < construct; ++i)
            {
                var constructed = WrappedConstruct();
                Return(constructed);
            }
        }

        public virtual T Borrow()
        {
            return WrappedBorrow();
        }

        public virtual void Return(T item)
        {
            WrappedReturn(item);
        }

        public virtual void Clear()
        {
            while (_pool.TryDequeue(out var item))
            {
                WrappedDestroy(item);
            }
        }

        public void Dispose()
        {
            Clear();
        }

        protected T WrappedBorrow()
        {
            if (_pool.Count == 0)
            {
                return WrappedConstruct();
            }
            return _pool.Dequeue();
        }

        protected T WrappedConstruct()
        {
            _constructed += 1;
            return Construct();
        }

        protected virtual T Construct()
        {
            return Object.Instantiate(_prefab, _parent, false);
        }

        protected void WrappedReturn(T item)
        {
            if (_pool.Count >= _capacity)
            {
                WrappedDestroy(item);
            }
            else
            {
                _pool.Enqueue(item);
            }
        }

        protected void WrappedDestroy(T item)
        {
            _constructed -= 1;
            Destroy(item);
        }

        protected virtual void Destroy(T item)
        {
            Object.Destroy(item);
        }
    }
}