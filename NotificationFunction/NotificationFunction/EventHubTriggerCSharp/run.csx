#r "Newtonsoft.Json"
using System;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using System.Net;

public static async Task<HttpResponseMessage> Run(string myEventHubMessage, TraceWriter log)
{
    try
    {
        if (myEventHubMessage.StartsWith("["))
        {
            dynamic msgs = JArray.Parse(myEventHubMessage);
            foreach (dynamic msg in msgs)
            {
                if (msg.data == "DroneStart")
                {
                    await send(msg.ToString(), log);
                    break;
                }
            }
        }
        else
        {
            dynamic msg = JObject.Parse(myEventHubMessage);
            if (msg.data == "DroneStart")
            {
                await send(myEventHubMessage, log);
            }
        }
    }
    catch (Microsoft.Azure.Devices.Common.Exceptions.DeviceMaximumQueueDepthExceededException qex)
    {
        log.Info($"{qex.Message}");
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
    catch (Exception ex)
    {
        log.Info($"Error: {ex.GetType()} {ex.Message}\n\t message: {myEventHubMessage}");
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
    return new HttpResponseMessage(HttpStatusCode.OK);
}

static async Task send(string msg, TraceWriter log)
{
    var c2dconn = GetEnvironmentVariable("iothubconnectionstring");
    using (ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(c2dconn.Trim(), Microsoft.Azure.Devices.TransportType.Amqp))
    {
        var message = new { data = "DroneStart", timestamp = DateTime.UtcNow };
        await serviceClient.SendAsync("Drone", new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))));
        log.Info($"Message sent to Drone: {msg}");
    }
}
public static string GetEnvironmentVariable(string name)
{
    return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
}