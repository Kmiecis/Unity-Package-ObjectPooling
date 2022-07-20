using System;

namespace Common.Pooling
{
    public interface IRepoolable<T> : IDisposable
    {
        T Value { get; }
    }
}
