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
                .AddJsonFile("localAppSettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            var s_deviceClient = DeviceClient.CreateFromConnectionString(configuration.GetConnectionString("IoTHubConnectionString"), Microsoft.Azure.Devices.Client.TransportType.Mqtt);


            while (true)
            {
                await SendSimulatedSensorData(s_deviceClient);
                await Task.Delay(10000);
            }


            //await SendSensorData(s_deviceClient);
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

        public static String ReadLastLine(string path)
        {
            return ReadLastLine(path, Encoding.ASCII, "\n");
        }

        public static String ReadLastLine(string path, Encoding encoding, string newline)
        {
            int charsize = encoding.GetByteCount("\n");

            byte[] buffer = new byte[charsize];

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var length = stream.Length;
                stream.Seek(-10, SeekOrigin.End);

                while(encoding.GetString(buffer) != "\n")
                {
                    stream.Seek(-2, SeekOrigin.Current);
                    stream.Read(buffer, 0, 1);
                }
                buffer = new byte[length - stream.Position];
                stream.Read(buffer, 0, buffer.Length);         
            }
            return encoding.GetString(buffer);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }

    public class SensorData
    {
        public DateTime TimeStamp { get; set; }
        public double CO2 { get; set; }

        public double Temp { get; set; }
        
    }
}
