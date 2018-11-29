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
        public UInt32 nPinCount = 0;
        public static int groupSelected = 1;
        public UInt32 bDioDisable = 0;

        [DllImport("aaeonEAPI.dll", EntryPoint = "EApiGPIOSetDirection")]
        public static extern UInt32 EApiGPIOSetDirection(UInt32 Id, UInt32 Bitmask, UInt32 Direction);

        [DllImport("aaeonEAPI.dll", EntryPoint = "EApiGPIOSetLevel")]
        public static extern UInt32 EApiGPIOSetLevel(UInt32 Id, UInt32 Bitmask, UInt32 Level);

        [DllImport("aaeonEAPI.dll", EntryPoint = "EApiLibInitialize")]
        public static extern UInt32 EApiLibInitialize();

        [DllImport("aaeonEAPI.dll", EntryPoint = "EApiLibUnInitialize")]
        public static extern UInt32 EApiLibUnInitialize();

        private static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("localAppSettings.json", optional: true, reloadOnChange: true);

            configuration = builder.Build();

            var s_deviceClient = DeviceClient.CreateFromConnectionString(configuration.GetConnectionString("IoTHubConnectionString"), Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            // Create a handler for the direct method call
            s_deviceClient.SetMethodHandlerAsync("StartAlarm", StartAlarm, null).Wait();

            while (true)
            {
                await SendSimulatedSensorData(s_deviceClient);
                await Task.Delay(10000);
            }


            //await SendSensorData(s_deviceClient);
        }

        /// <summary>
        /// Sets value on GPIO PIN
        /// </summary>
        /// <param name="dPin">number of pin</param>
        /// <param name="nInput">iput/output  1/0</param>
        /// <param name="nHigh">on/off 1/0</param>
        public static void SetDioPinState(UInt32 dPin, UInt32 nInput, UInt32 nHigh)
        {
            UInt32 err1 = EAPI.EAPI_STATUS_SUCCESS;
            UInt32 err2 = EAPI.EAPI_STATUS_SUCCESS;

            err1 = EApiGPIOSetDirection(EAPI.EAPI_GPIO_GPIO_ID((UInt32)(dPin + (8 * (groupSelected - 1)))), 0xFFFFFFFF, nInput);
            err2 = EApiGPIOSetLevel(EAPI.EAPI_GPIO_GPIO_ID((UInt32)(dPin + (8 * (groupSelected - 1)))), 0xFFFFFFFF, nHigh);

            if (err1 != EAPI.EAPI_STATUS_SUCCESS || err2 != EAPI.EAPI_STATUS_SUCCESS)
            {
                if (err1 == EAPI.EAPI_STATUS_DEVICE_NOT_READY || err2 == EAPI.EAPI_STATUS_DEVICE_NOT_READY)
                {
                    Console.WriteLine("Can't set DIO" + (dPin + 1 + (8 * (groupSelected - 1))).ToString() + " value:\nHardware not ready. Please check BIOS setting.");
                }
                else
                {
                    Console.WriteLine("Can't set DIO value.");
                }
            }
        }

        private static Task<MethodResponse> StartAlarm(MethodRequest methodRequest, object userContext)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(methodRequest.DataAsJson);
            Console.WriteLine("ALARM CO2 :" + (string)jsonObject.CO2);

            // Initiate alar for 5 seconds
            Task.Run(async () =>
            {
                SetDioPinState(2, 0, 1);
                Console.WriteLine("Alarm START");
                await Task.Delay(5000);
                Console.WriteLine("Alarm STOP");
                SetDioPinState(2, 0, 0);

            });


            //respond to IoT Hub
            string result = "{\"result\":\"Executed direct method: " + methodRequest.Name + "\"}";
            return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));
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
