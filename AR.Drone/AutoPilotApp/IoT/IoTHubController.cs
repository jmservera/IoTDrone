using AR.Drone.Client;
using AutoPilotApp.Common;
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
        DeviceClient ioTclient;
        DroneClient droneClient;
        public IoTHubController(DroneClient client)
        {
            droneClient = client;
            droneClient.NavigationDataAcquired += DroneClient_NavigationDataAcquired;
            init();
        }

        async void init()
        {
            try
            {
                await Task.Run(() =>
                {
                    var connString = ConfigurationManager.AppSettings["IoTDevice"];
                    Logger.LogInfo($"Connecting to IoT Hub {connString}");
                    ioTclient = DeviceClient.CreateFromConnectionString(connString, TransportType.Mqtt);
                    Logger.LogInfo($"Connected to IoT Hub");
                });
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void DroneClient_NavigationDataAcquired(AR.Drone.Data.Navigation.NavigationData obj)
        {
        }
    }
}
