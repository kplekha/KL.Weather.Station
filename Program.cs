using static System.Collections.Specialized.BitVector32;

namespace KL.Weather.Station
{
    internal class Program
    {
        static async Task Main()
        {
            var publisher = new WeatherPublisher();
            var display = new DisplaySubscriber();
            var alert = new AlertSubscriber(0,30);

            display.Subscribe(publisher);
            alert.Subscribe(publisher);

            await publisher.StreamTemperatureAsync(SimulateTemperatureSensor());

            
            await publisher.EndTransmissionAsync();
        }


        static async IAsyncEnumerable<double> SimulateTemperatureSensor()
        {
            var rand = new Random();
            while (true)
            {
                //delay
                await Task.Delay(1000);
                yield return rand.Next(-5, 50);
            }
        }
    }
}
