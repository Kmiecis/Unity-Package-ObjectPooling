namespace Common.Pooling
{
    /// <summary>
    /// <see cref="ARepoolablePool{T}"/> using <see cref="Repoolable{T}"/> wrapper
    /// </summary>
    public class RepoolablePool<T> : ARepoolablePool<T>
    {
        public RepoolablePool(int capacity, IPool<T> subpool) :
            base(capacity, subpool)
        {
        }

        public override IRepoolable<T> Construct()
        {
            return new Repoolable<T>(_subpool);
        }

        public override void Destroy(IRepoolable<T> item)
        {
            item.Dispose();
        }
    }
}
