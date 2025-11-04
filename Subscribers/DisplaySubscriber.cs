using KL.Weather.Station.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KL.Weather.Station.Subscribers
{
    public class DisplaySubscriber : IAsyncObserver<double>
    {
        private IDisposable _unsubscriber;

        public void Subscribe(WeatherPublisher station)
        {
            _unsubscriber = station.Subscribe(this);
        }

        public Task OnNextAsync(double temperature)
        {
            Console.WriteLine($"[DisplayPanel] Current temperature: {temperature}°C");
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            Console.WriteLine($"[DisplayPanel] Error: {ex.Message}");
            return Task.CompletedTask;
        }

        public Task OnCompletedAsync()
        {
            Console.WriteLine("[DisplayPanel] No more temperature updates.");
            Unsubscribe();
            return Task.CompletedTask;
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
    }
}
