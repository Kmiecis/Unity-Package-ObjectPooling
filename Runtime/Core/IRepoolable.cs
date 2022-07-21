using System;

namespace Common.Pooling
{
    /// <summary>
    /// Base 
    /// </summary>
    public interface IRepoolable<T> : IDisposable
    {
        T Value { get; }
    }
}
