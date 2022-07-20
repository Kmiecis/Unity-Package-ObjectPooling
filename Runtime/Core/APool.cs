using System.Collections.Generic;

namespace Common.Pooling
{
    public abstract class APool<T> : IPool<T>
    {
        protected readonly int _capacity;
        protected readonly Queue<T> _pool;

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
            for (int i = 0; i < count; ++i)
            {
                Return(WrappedConstruct());
            }
        }
        
        public virtual T Borrow()
        {
            if (_pool.Count == 0)
            {
                return WrappedConstruct();
            }
            return _pool.Dequeue();
        }

        public void Borrow(T[] target)
        {
            for (int i = 0; i < target.Length; ++i)
            {
                target[i] = Borrow();
            }
        }

        public virtual void Return(T item)
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

        public virtual void Dispose()
        {
            Clear();
        }
    }
}
