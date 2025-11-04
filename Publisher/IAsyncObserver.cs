namespace KL.Weather.Station.Publisher
{
    public interface IAsyncObserver<T>
    {
        Task OnNextAsync(T value);
        Task OnErrorAsync(Exception ex);
        Task OnCompletedAsync();
    }
}

