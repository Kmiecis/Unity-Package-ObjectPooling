namespace Common.Pooling
{
    public abstract class AReusablePool<T> : APool<T>
        where T : IReusable
    {
        public AReusablePool(int capacity) :
            base(capacity)
        {
        }

        public override T Borrow()
        {
            var item = base.Borrow();
            item.OnBorrow();
            return item;
        }

        public override void Return(T item)
        {
            item.OnReturn();
            base.Return(item);
        }
    }
}
