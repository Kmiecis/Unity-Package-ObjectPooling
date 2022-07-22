namespace Common.Pooling
{
    /// <summary>
    /// Base interface for poolable items with custom on borrow / on return logic
    /// </summary>
    public interface IReusable
    {
        /// <summary>
        /// Called just before leaving pool
        /// </summary>
        void OnBorrow();

        /// <summary>
        /// Called just before returning to pool
        /// </summary>
        void OnReturn();
    }
}
