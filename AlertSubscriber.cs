

namespace KL.Weather.Station
{
    public class AlertSubscriber : IAsyncObserver<double>
    {
        private IDisposable _unsubscriber;
        private readonly double _lowThreshold;
        private readonly double _highThreshold;

        public AlertSubscriber(double low, double high)
        {
            _lowThreshold = low;
            _highThreshold = high;
        }

        public void Subscribe(WeatherPublisher station)
        {
            _unsubscriber = station.Subscribe(this);
        }

        public Task OnNextAsync(double temperature)
        {
            if (temperature < _lowThreshold)
                Console.WriteLine($"[ALERT] Very cold! {temperature}°C");
            else if (temperature > _highThreshold)
                Console.WriteLine($"[ALERT] Very hot! {temperature}°C");
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            Console.WriteLine($"[AlertSystem] Error: {ex.Message}");
            return Task.CompletedTask;
        }

        public Task OnCompletedAsync()
        {
            Console.WriteLine("[AlertSystem] Weather station stopped sending updates.");
            Unsubscribe();
            return Task.CompletedTask;
        }
        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

    }
}
