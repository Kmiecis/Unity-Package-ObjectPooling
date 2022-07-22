namespace Common.Pooling
{
    /// <summary>
    /// Base <see cref="IRepoolable{T}"/> implementation
    /// </summary>
    public class Repoolable<T> : IRepoolable<T>
    {
        protected readonly IPool<T> _pool;
        protected T _value;

        public Repoolable(IPool<T> pool)
        {
            _pool = pool;
        }

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    _value = _pool.Borrow();
                }
                return _value;
            }
        }
        
        public void Dispose()
        {
            if (_value != null)
            {
                _pool.Return(_value);
            }
            _value = default;
        }

        public static implicit operator T(Repoolable<T> repoolable)
        {
            return repoolable.Value;
        }
    }
}
