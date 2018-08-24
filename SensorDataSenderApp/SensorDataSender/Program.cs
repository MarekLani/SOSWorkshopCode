using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp
{
    class Program
    {
        private static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            var s_deviceClient = DeviceClient.CreateFromConnectionString(configuration.GetConnectionString("IoTHubConnectionString"), Microsoft.Azure.Devices.Client.TransportType.Mqtt);

            while (true)
            {
                await SendSimulatedSensorData(s_deviceClient);
                await Task.Delay(10000);
            }
        }

        private static async Task SendSimulatedSensorData(DeviceClient deviceClient)
        {
            SensorData sd = new SensorData();
            sd.TimeStamp = DateTime.Now;
            Random r = new Random();
            sd.CO2 = r.NextDouble()*100;
            sd.Temp = r.NextDouble() * 100;

            var message = JsonConvert.SerializeObject(sd);

            Console.WriteLine(message);
            await deviceClient.SendEventAsync(new Message(Encoding.ASCII.GetBytes(message)));
        }
    }

    public class SensorData
    {
        public DateTime TimeStamp { get; set; }
        public double CO2 { get; set; }

        public double Temp { get; set; }
        
    }
}
