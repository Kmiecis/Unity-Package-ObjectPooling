namespace Common
{
    public static class Extensions
    {
        #region IPool
        public static Pooling.Repoolable<T> AsRepoolable<T>(this Pooling.IPool<T> self)
            where T : class
        {
            return new Pooling.Repoolable<T>(self);
        }

        public static void Borrow<T>(this Pooling.IPool<T> self, T[] target)
        {
            for (int i = 0; i < target.Length; ++i)
            {
                target[i] = self.Borrow();
            }
        }

        public static void Return<T>(this Pooling.IPool<T> self, T[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                self.Return(items[i]);
            }
        }

        public static void Return<T>(this Pooling.IPool<T> self, ref T item)
            where T : class
        {
            if (item != null)
            {
                self.Return(item);
                item = null;
            }
        }
        #endregion
    }
}
