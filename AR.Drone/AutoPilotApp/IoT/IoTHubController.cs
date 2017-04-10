using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPilotApp.IoT
{
    public class IoTHubController
    {
        DeviceClient client;
        public IoTHubController()
        {
            var connString = ConfigurationManager.AppSettings["IoTDevice"];
            client = DeviceClient.CreateFromConnectionString(connString);
        }
    }
}
