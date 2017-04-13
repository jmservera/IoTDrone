using AR.Drone.Client;
using AutoPilotApp.Common;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
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
                    deviceClient.RetryPolicy = RetryPolicyType.Exponential_Backoff_With_Jitter;
                    cancelTokenSource = new CancellationTokenSource();
                    startMessageReceiver(cancelTokenSource.Token);
                    Logger.LogInfo($"Connected to IoT Hub");
                    //twin = await deviceClient.GetTwinAsync();
                    //Logger.LogInfo($"Twin received {twin.DeviceId}");
                    //await deviceClient.OpenAsync();

                });
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void startMessageReceiver(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    var message = await deviceClient.ReceiveAsync(TimeSpan.FromMilliseconds(1000));
                    if (message != null)
                    {
                        try
                        {
                            var str = Encoding.UTF8.GetString(message.GetBytes());
                            dynamic obj = JsonConvert.DeserializeObject(str);
                            if (obj.data != null)
                            {
                                var x = (DateTime)obj.timestamp;
                                if (DateTime.UtcNow - x > TimeSpan.FromSeconds(60))
                                {
                                    Logger.LogError($"Old message received: {str}");
                                }
                                else
                                {
                                    var droneMsg = obj.data.ToString();
                                    if (!string.IsNullOrEmpty(droneMsg) && droneMsg.ToUpper()=="DRONESTART")
                                    {
                                        Logger.Log($"{x.ToLocalTime()} received: {droneMsg}", LogLevel.Event);
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Logger.LogException(ex);
                        }
                        finally
                        {
                            await deviceClient.CompleteAsync(message);
                        }
                    }
                    Thread.Sleep(10);
                }
            }, token);
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
                            obj.Altitude,
                            State = obj.State.ToString(),
                            Timestamp= DateTime.UtcNow
                        };
                        var infoString = JsonConvert.SerializeObject(info);

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
