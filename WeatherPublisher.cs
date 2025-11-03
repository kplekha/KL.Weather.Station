using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KL.Weather.Station
{

    public partial class WeatherPublisher 
    {
        private readonly List<IAsyncObserver<double>> _observers = new();
        private double _temperature;

        public IDisposable Subscribe(IAsyncObserver<double> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            
            return new Unsubscriber(_observers, observer);
        }       

        public async Task StreamTemperatureAsync(IAsyncEnumerable<double> tempStream)
        {
            await foreach (var temp in tempStream)
            {
                foreach (var obs in _observers)
                {
                    _temperature = temp;
                    await obs.OnNextAsync(temp);                    
                }
            }
        }
      
        public async Task EndTransmissionAsync()
        {
            var tasks = new List<Task>();
            foreach (var observer in _observers.ToArray())
            {
                tasks.Add(observer.OnCompletedAsync());
            }
            await Task.WhenAll(tasks);
            _observers.Clear();
        }
    }
}

