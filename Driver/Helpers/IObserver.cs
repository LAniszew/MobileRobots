namespace Driver.Helpers
{
    public interface IObserver<in T>
    {
        void GetNotified(T data);
    }
}
