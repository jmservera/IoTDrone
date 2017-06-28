using AR.Drone.Client;
using AutoPilotApp.Common;
using AutoPilotApp.Models;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AutoPilotApp.IoT
{
    public class IoTHubController
    {
        DeviceClient deviceClient;
        DroneClient droneClient;
        AnalyzerOutput analyzerOutput;
        Bitmaps bitmaps;

        public IoTHubController(DroneClient client, AnalyzerOutput output, Bitmaps bitmaps)
        {
            this.bitmaps = bitmaps;
            droneClient = client;
            this.analyzerOutput = output;
            droneClient.NavigationDataAcquired += DroneClient_NavigationDataAcquired;
            cancelTokenSource = new CancellationTokenSource();
            init(cancelTokenSource.Token);
        }

        string deviceId;

        CancellationTokenSource cancelTokenSource;

        async void init(CancellationToken token)
        {
            try
            {
                await Task.Run(() =>
               {
                   var connString = ConfigurationManager.AppSettings["IoTDevice"];
                   Logger.LogInfo($"Connecting to IoT Hub {connString}");
                   var builder = IotHubConnectionStringBuilder.Create(connString);
                   deviceId = builder.DeviceId;
                   deviceClient = DeviceClient.CreateFromConnectionString(connString, TransportType.Mqtt);
                   deviceClient.RetryPolicy = RetryPolicyType.Exponential_Backoff_With_Jitter;
                   startMessageReceiver(cancelTokenSource.Token);
                   Logger.LogInfo($"Connected to IoT Hub");
               }, token);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        async void startMessageReceiver(CancellationToken token)
        {
            try
            {
                await Task.Run(async () =>
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
                                    analyzerOutput.Start = false;

                                    var x = (DateTime)obj.timestamp;
                                    if (DateTime.UtcNow - x > TimeSpan.FromSeconds(60))
                                    {
                                        Logger.LogError($"Old message received: {str}");
                                    }
                                    else
                                    {
                                        var droneMsg = obj.data.ToString();
                                        if (!string.IsNullOrEmpty(droneMsg) && droneMsg.ToUpper() == "DRONESTART")
                                        {
                                            analyzerOutput.Start = true;
                                            Logger.Log($"{x.ToLocalTime()} received: {droneMsg}", LogLevel.Event);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
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
            catch (Exception oex)
            {
                Logger.LogException(oex);
            }
        }

        DateTime last = DateTime.Now;
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
                            Timestamp = DateTime.UtcNow
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

        public async Task SendPictureAsync()
        {
            await bitmaps.First.Dispatcher.InvokeAsync(async () =>
            {
                try
                {
                    var encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmaps.First));
                    using (var stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        stream.Position = 0;
                        await deviceClient.UploadToBlobAsync("picture.jpg", stream);
                    }
                    Logger.Log("Picture sent", LogLevel.Event);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                }
            });

            var message = JsonConvert.SerializeObject(new
            {
                path = "https://dronedemoevents900b.blob.core.windows.net/pictures/drone001/picture.jpg",
                timestamp = DateTime.UtcNow
            });

            Message msg = new Message(Encoding.UTF8.GetBytes(message));
            msg.Properties.Add("process", "defectq");
            await deviceClient.SendEventAsync(msg);
        }
    }
}
