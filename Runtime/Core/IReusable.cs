namespace Common.Pooling
{
    public interface IReusable
    {
        void OnBorrow();

        void OnReturn();
    }
}
