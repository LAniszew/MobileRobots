namespace Driver.Helpers
{
    public interface IObservable<T>
    {
        void Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
        void NotifyObservers(T data);
    }
}
