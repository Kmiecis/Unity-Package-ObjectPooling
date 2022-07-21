namespace Common.Pooling
{
    public abstract class ARepoolablePool<T> : APool<IRepoolable<T>>
    {
        protected readonly IPool<T> _subpool;

        public ARepoolablePool(int capacity, IPool<T> subpool) :
            base(capacity)
        {
            _subpool = subpool;
        }

        public override void Dispose()
        {
            _subpool.Dispose();
            base.Dispose();
        }
    }
}
