using System;

namespace Common.Pooling
{
    /// <summary>
    /// Base interface for handling poolable items of many types
    /// </summary>
    public interface IMultiPool<T> : IDisposable
    {
        /// <summary>
        /// Call to borrow another item
        /// </summary>
        U Borrow<U>() where U : T;

        /// <summary>
        /// Call to return another item
        /// </summary>
        void Return<U>(U item) where U : T;
    }
}
