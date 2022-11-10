using System;

namespace Common.Pooling
{
    /// <summary>
    /// <see cref="APool{T}"/> with custom delegate support
    /// </summary>
    public sealed class DelegatePool<T> : APool<T>
        where T : class
    {
        private Func<T> _constructor;
        private Action<T> _destructor;
        private Action<T> _onBorrow;
        private Action<T> _onReturn;

        public DelegatePool(Func<T> constructor, Action<T> destructor, int capacity) :
            base(capacity)
        {
            _constructor = constructor;
            _destructor = destructor;
        }

        public Action<T> OnBorrow
        {
            set => _onBorrow = value;
        }

        public Action<T> OnReturn
        {
            set => _onReturn = value;
        }

        protected override T Construct()
        {
            return _constructor();
        }

        protected override void Destroy(T item)
        {
            _destructor(item);
        }

        public override T Borrow()
        {
            var item = base.Borrow();
            if (_onBorrow != null)
                _onBorrow(item);
            return item;
        }

        public override void Return(T item)
        {
            if (_onReturn != null)
                _onReturn(item);
            base.Return(item);
        }
    }
}
