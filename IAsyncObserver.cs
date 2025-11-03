namespace KL.Weather.Station
{
    public interface IAsyncObserver<T>
    {
        Task OnNextAsync(T value);
        Task OnErrorAsync(Exception ex);
        Task OnCompletedAsync();
    }
}

