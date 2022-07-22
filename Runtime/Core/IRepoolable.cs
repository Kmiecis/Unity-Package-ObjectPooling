using System;

namespace Common.Pooling
{
    /// <summary>
    /// Base interface of a wrapper for poolable items easing returning to their respective pool
    /// </summary>
    public interface IRepoolable<T> : IDisposable
    {
        /// <summary>
        /// Call to borrow and get item
        /// </summary>
        T Value { get; }
    }
}
