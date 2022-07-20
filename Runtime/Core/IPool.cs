using System;

namespace Common.Pooling
{
    public interface IPool<T> : IDisposable
    {
        T Borrow();

        void Return(T item);
    }
}
