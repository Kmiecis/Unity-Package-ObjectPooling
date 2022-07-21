using System;

namespace Common.Pooling
{
    /// <summary>
    /// Base interface for handling poolable items
    /// </summary>
    public interface IPool<T> : IDisposable
    {
        /// <summary>
        /// Call to borrow another item
        /// </summary>
        T Borrow();

        /// <summary>
        /// Call to return another item
        /// </summary>
        void Return(T item);
    }
}
