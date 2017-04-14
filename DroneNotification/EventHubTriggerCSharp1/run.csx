#r "Newtonsoft.Json"

using System;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Devices;
using System.Net;

public static async Task<HttpResponseMessage> Run(string myEventHubMessage, TraceWriter log)
{
    try
    {
        var c2dconn=GetEnvironmentVariable("iothubconnectionstring");
        ServiceClient serviceClient=ServiceClient.CreateFromConnectionString(c2dconn.Trim(), Microsoft.Azure.Devices.TransportType.Amqp);
        if(myEventHubMessage.StartsWith("[")){
            dynamic msgs=JArray.Parse(myEventHubMessage);
            foreach(dynamic msg in msgs){
                if(msg.data=="DroneStart"){
                    log.Info($"************* data {msg.data} {msg.ToString()}");
                    await serviceClient.SendAsync("Drone", new Message(Encoding.UTF8.GetBytes(msg.ToString())));
                    break;
                }
            }
        }        
        else
        {        
            dynamic msg= JObject.Parse(myEventHubMessage);
            if(msg.data=="DroneStart")
            {
                log.Info($"*************        data {msg.data}");
                await serviceClient.SendAsync("Drone", new Message(Encoding.UTF8.GetBytes(myEventHubMessage)));
            }
        }
    }
    catch(Exception ex)
    {
        log.Info($"Error: {ex.Message}\n\t message: {myEventHubMessage}");
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }
    return new HttpResponseMessage(HttpStatusCode.OK);
}

public static string GetEnvironmentVariable(string name)
{
    return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
}