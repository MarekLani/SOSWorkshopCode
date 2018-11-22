using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AlertProcessing
{
    public static class AlertProcessingFunction
    {
        [FunctionName("AlertProcessingFunction")]
        public static async Task Run([EventHubTrigger("alerteh", Connection = "EventHubConnectionString")]string myEventHubMessage, ILogger log)
        {
            try
            {
                var s_serviceClient = ServiceClient.CreateFromConnectionString(System.Environment.GetEnvironmentVariable(
                    "IoTHubConnectionString", EnvironmentVariableTarget.Process));

                dynamic jsonObject = JsonConvert.DeserializeObject(myEventHubMessage);

                var methodInvocation = new CloudToDeviceMethod("StartAlarm") { ResponseTimeout = TimeSpan.FromSeconds(30) };
                methodInvocation.SetPayloadJson(@"{""CO2"":""" + (string)jsonObject.avgco2 + @"""}");
                //methodInvocation.SetPayloadJson("10");
                // Invoke the direct method asynchronously and get the response from the simulated device.
                var response = await s_serviceClient.InvokeDeviceMethodAsync((string)jsonObject.deviceid, methodInvocation);

                log.LogInformation("Response status: {0}, payload:", response.Status);
                log.LogInformation(response.GetPayloadAsJson());
            }
            catch (Exception)
            {

            }
        }
    }
}
