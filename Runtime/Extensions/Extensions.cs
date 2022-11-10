namespace Common.Pooling
{
    public static class Extensions
    {
        #region IPool
        public static Repoolable<T> AsRepoolable<T>(this IPool<T> self)
            where T : class
        {
            return new Repoolable<T>(self);
        }
        #endregion
    }
}
