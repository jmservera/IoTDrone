using AR.Drone.Avionics;
using AR.Drone.Avionics.Objectives;
using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data.Navigation;
using AutoPilotApp.Common;
using AutoPilotApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoPilotApp.Pilot
{
    public class Controller : ObservableObject
    {
        DroneClient droneClient;
        private Missions currentMission;

        public Missions CurrentMission
        {
            get { return currentMission; }
        }

        Autopilot autoPilot;
        AnalyzerOuput analyzer;
        public Controller(DroneClient client, AnalyzerOuput output)
        {
            analyzer = output;
            droneClient = client;
            autoPilot = new Autopilot(client);
        }

        public void EmergencyStop()
        {
            stopAutopilot();

            droneClient.Emergency();
        }

        public bool Stop()
        {
            try
            {
                stopAutopilot();
                //droneClient.Hover();
                if (loop != null)
                {
                    loop.Wait(1000);
                    loop = null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return false;
            }
            return true;
        }

        void stopAutopilot()
        {
            if (cancellationTokenSource != null)
            {
                lock (this)
                {
                    if (cancellationTokenSource != null)
                    {
                        cancellationTokenSource.Cancel();
                        cancellationTokenSource = null;
                    }
                }
            }
        }

        public void Start(Missions mission)
        {
            stopAutopilot();

            currentMission = mission;

            RaisePropertyChanged(nameof(CurrentMission));

            startAutopilot();

        }

        CancellationTokenSource cancellationTokenSource;
        Task loop;
        void startAutopilot()
        {
            if (cancellationTokenSource != null)
                return;
            lock (this)
            {
                if (cancellationTokenSource != null)
                    return;

                cancellationTokenSource = new CancellationTokenSource();
                loop = Task.Run(() => Loop(cancellationTokenSource.Token), cancellationTokenSource.Token);
            }
        }

        void Loop(CancellationToken token)
        {
            step = 0;
            Stopwatch sw1 = null;

            while (!token.IsCancellationRequested)
            {
                switch (currentMission)
                {
                    case Missions.Objective:
                    case Missions.Home:
                        switch (step)
                        {
                            case 0:
                                flyToObjective();
                                break;
                            case 1:
                                if (sw1 == null)
                                {
                                    sw1 = Stopwatch.StartNew();
                                }
                                if (sw1.ElapsedMilliseconds < 4000)
                                {
                                    hover();
                                }
                                else
                                {
                                    Logger.LogInfo("Step 2");

                                    step = 2;
                                    droneClient.Land();
                                    Stop();
                                }
                                break;
                            default:
                                hover();
                                break;
                        }
                        break;
                    case Missions.AttendeesPicture:
                    default:
                        {
                            CurrentCommand = "Hover";
                            hover();
                            break;
                        }
                }
                Task.Delay(10).Wait();
            }
        }

        private void hover()
        {
            droneClient.Hover();
        }

        private string currentCommand;

        public string CurrentCommand
        {
            get { return currentCommand; }
            set { Set(ref currentCommand, value); }
        }

        int step;

        void flyToObjective()
        {
            bool flight = true;
            var state = droneClient.NavigationData.State;
            if (state.HasFlag(NavigationState.Emergency))
                return;

            if (flight && state.HasFlag(NavigationState.Landed))
            {
                analyzer.ResultingCommand = "takeof";
                droneClient.Takeoff();
                return;
            }
            if (analyzer.Detected)
            {
                if (droneClient.NavigationData.Altitude > 0.5 || !flight)
                {
                    var width = analyzer.FovSize.Width / 2f;
                    var change = width - analyzer.Center.X;

                    var diff = (analyzer.Distance / analyzer.FovSize.Width) * 7.5f;

                    if (change > diff)
                    {
                        if (flight)
                            droneClient.Progress(FlightMode.Progressive, roll: -0.10f, yaw: -0.05f);
                        analyzer.ResultingCommand = $"right {change} {diff}";
                    }
                    else if  (change < (0-diff))
                    {
                        if (flight)
                            droneClient.Progress(FlightMode.Progressive, roll: 0.10f, yaw: 0.05f);
                        analyzer.ResultingCommand = $"left {change} {diff}";
                    }
                    else
                    {
                        var d = analyzer.Distance / analyzer.FovSize.Width;
                        if (analyzer.Distance > 0 && (analyzer.Distance/analyzer.FovSize.Width ) < 15)
                        {
                            analyzer.ResultingCommand = $"pitch {change} {d}";

                            if (flight)
                                droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
                        }
                        else
                        {
                            analyzer.ResultingCommand = "hover";
                            step = 1;
                            Logger.LogInfo("Step 1");
                        }
                    }
                }
                else
                {
                    droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
                    analyzer.ResultingCommand = $"altitude {droneClient.NavigationData.Altitude}";
                }
            }
            else
            {
                analyzer.ResultingCommand = "seek";

                if (flight)
                    droneClient.Progress(FlightMode.Progressive, yaw: 0.10f);
            }

        }
    }
}
