namespace Common.Pooling
{
    /// <summary>
    /// <see cref="APool{T}"/> handling new() types
    /// </summary>
    public class Pool<T> : APool<T>
        where T : new()
    {
        public Pool(int capacity) :
            base(capacity)
        {
        }

        protected override T Construct()
        {
            return new T();
        }

        protected override void Destroy(T item)
        {
            // no-op
        }
    }
}
