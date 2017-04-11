using AR.Drone.Client;
using AutoPilotApp.Common;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AutoPilotApp.IoT
{
    public class IoTHubController
    {
        DeviceClient deviceClient;
        DroneClient droneClient;

        public IoTHubController(DroneClient client)
        {
            droneClient = client;
            droneClient.NavigationDataAcquired += DroneClient_NavigationDataAcquired;
            init();
        }
        Twin twin;
        string deviceId;

        CancellationTokenSource cancelTokenSource;

        async void init()
        {
            try
            {
                await Task.Run(async () =>
                {
                    var connString = ConfigurationManager.AppSettings["IoTDevice"];
                    Logger.LogInfo($"Connecting to IoT Hub {connString}");
                    var builder = IotHubConnectionStringBuilder.Create(connString);
                    deviceId = builder.DeviceId;
                    deviceClient = DeviceClient.CreateFromConnectionString(connString, TransportType.Mqtt);
                    Logger.LogInfo($"Connected to IoT Hub");
                    twin = await deviceClient.GetTwinAsync();
                    Logger.LogInfo($"Twin received {twin.DeviceId}");
                    await deviceClient.OpenAsync();
                    
                });
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        DateTime last=DateTime.Now;
        async void DroneClient_NavigationDataAcquired(AR.Drone.Data.Navigation.NavigationData obj)
        {
            if (deviceClient != null)
            {
                if (DateTime.Now.Subtract(last).TotalMilliseconds > 1000)
                {
                    last = DateTime.Now;
                    try
                    {
                        var info = new
                        {
                            DeviceID = deviceId,
                            Battery = obj.Battery.Percentage,
                            obj.Yaw,
                            obj.Pitch,
                            obj.Roll,
                            State = obj.State.ToString(),
                            Timestamp= DateTime.UtcNow
                        };
                        var infoString = Newtonsoft.Json.JsonConvert.SerializeObject(info);

                        Message msg = new Message(Encoding.UTF8.GetBytes(infoString));
                        msg.Properties.Add("type", "telemetry");
                        await deviceClient.SendEventAsync(msg).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                    }
                }
            }
        }
    }
}
