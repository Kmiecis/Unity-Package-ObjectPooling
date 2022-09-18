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
        protected int _capacity;
        protected int _constructed;

        public APool(int capacity)
        {
            _capacity = capacity;
            _pool = new Queue<T>(capacity);
        }

        public abstract T Construct();

        public abstract void Destroy(T item);

        protected T WrappedConstruct()
        {
            _constructed += 1;
            return Construct();
        }

        protected void WrappedDestroy(T item)
        {
            _constructed -= 1;
            Destroy(item);
        }
        
        public void Initialize(int count)
        {
            var constructed = Math.Min(count, _capacity);
            for (int i = 0; i < constructed; ++i)
            {
                Return(WrappedConstruct());
            }
        }

        protected T WrappedBorrow()
        {
            if (_pool.Count == 0)
            {
                return WrappedConstruct();
            }
            return _pool.Dequeue();
        }
        
        public virtual T Borrow()
        {
            var value = WrappedBorrow();
            (value as IReusable)?.OnBorrow();
            return value;
        }

        public void Borrow(T[] target)
        {
            for (int i = 0; i < target.Length; ++i)
            {
                target[i] = Borrow();
            }
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

        public virtual void Return(T item)
        {
            (item as IReusable)?.OnReturn();
            WrappedReturn(item);
        }

        public void Return(T[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                Return(items[i]);
            }
        }

        public void Clear()
        {
            while (_pool.TryDequeue(out var item))
            {
                WrappedDestroy(item);
            }
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

        public virtual int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public virtual void Dispose()
        {
            Clear();
        }
    }
}
