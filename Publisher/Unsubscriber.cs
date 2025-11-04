using KL.Weather.Station.Publisher;

namespace KL.Weather.Station
{
    public partial class WeatherPublisher
    {
        private class Unsubscriber : IDisposable
        {
            private readonly List<IAsyncObserver<double>> _observers;
            private readonly IAsyncObserver<double> _observer;

            public Unsubscriber(List<IAsyncObserver<double>> observers, IAsyncObserver<double> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}

