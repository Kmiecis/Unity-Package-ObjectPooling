namespace Common.Pooling
{
    public class Pool<T> : APool<T>
        where T : new()
    {
        public Pool(int capacity) :
            base(capacity)
        {
        }

        public override T Construct()
        {
            return new T();
        }

        public override void Destroy(T item)
        {
            // Do nothing
        }
    }
}
