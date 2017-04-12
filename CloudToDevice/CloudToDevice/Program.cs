using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Devices;
using System.Net;

namespace CloudToDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        static async void Test()
        {
            try
            {
                var c2dconn = System.Configuration.ConfigurationManager.AppSettings["iothubconnectionstring"];

                log(c2dconn);
                ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(c2dconn, Microsoft.Azure.Devices.TransportType.Amqp);
                //dynamic msg = JObject.Parse(myEventHubMessage);
                var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
                await serviceClient.SendAsync("Drone", commandMessage);
            }
            catch (Exception ex)
            {
                log(ex.Message);
            }
        }

        static void log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
