namespace Common.Pooling
{
    public class RepoolablePool<T> : APool<IRepoolable<T>>
    {
        protected readonly IPool<T> _subpool;

        public RepoolablePool(int capacity, IPool<T> subpool) :
            base(capacity)
        {
            _subpool = subpool;
        }

        public override IRepoolable<T> Construct()
        {
            return new Repoolable<T>(_subpool);
        }

        public override void Destroy(IRepoolable<T> item)
        {
            item.Dispose();
        }

        public override void Dispose()
        {
            _subpool.Dispose();
            base.Dispose();
        }
    }
}
