using System;

namespace Common.Pooling
{
    /// <summary>
    /// <see cref="AReusablePool{T}"/> with custom delegate support
    /// </summary>
    public sealed class DelegateReusablePool<T> : AReusablePool<T>
        where T : class, IReusable
    {
        private Func<T> _constructor;
        private Action<T> _destructor;

        public DelegateReusablePool(int capacity, Func<T> constructor = null, Action<T> destructor = null) :
            base(capacity)
        {
            _constructor = constructor;
            _destructor = destructor;
        }

        public Func<T> Constructor
        {
            set => _constructor = value;
        }

        public Action<T> Destructor
        {
            set => _destructor = value;
        }

        public override T Construct()
        {
            return _constructor?.Invoke();
        }

        public override void Destroy(T item)
        {
            _destructor?.Invoke(item);
        }
    }
}
