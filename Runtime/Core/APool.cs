using System;
using System.Collections.Generic;

namespace Common.Pooling
{
    /// <summary>
    /// Base <see cref="IPool{T}"/> implementation
    /// </summary>
    public abstract class APool<T> : IPool<T>
    {
        protected readonly Queue<T> _pool;

        protected int _constructed;
        protected int _capacity;

        public APool() :
            this(-1)
        {
        }

        public APool(int capacity)
        {
            _pool = new Queue<T>(Math.Max(capacity, 4));
            _capacity = capacity;
        }

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

        public void Prewarm(int count)
        {
            var constructed = Math.Min(count, _capacity);
            for (int i = 0; i < constructed; ++i)
            {
                Return(WrappedConstruct());
            }
        }

        protected T WrappedConstruct()
        {
            _constructed += 1;

            return Construct();
        }

        protected abstract T Construct();

        protected void WrappedDestroy(T item)
        {
            _constructed -= 1;

            Destroy(item);
        }

        protected abstract void Destroy(T item);

        public void Borrow(T[] target)
        {
            for (int i = 0; i < target.Length; ++i)
            {
                target[i] = Borrow();
            }
        }

        public virtual T Borrow()
        {
            return WrappedBorrow();
        }

        protected T WrappedBorrow()
        {
            if (_pool.Count == 0)
            {
                return WrappedConstruct();
            }
            return _pool.Dequeue();
        }

        public void Return(T[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                Return(items[i]);
            }
        }

        public virtual void Return(T item)
        {
            WrappedReturn(item);
        }

        protected void WrappedReturn(T item)
        {
            if (_capacity > -1 && _pool.Count >= _capacity)
            {
                WrappedDestroy(item);
            }
            else
            {
                _pool.Enqueue(item);
            }
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
    }
}
