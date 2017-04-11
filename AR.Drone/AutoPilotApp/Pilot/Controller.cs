using AR.Drone.Avionics;
using AR.Drone.Avionics.Objectives;
using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data.Navigation;
using AutoPilotApp.Common;
using AutoPilotApp.Models;
using System;
using System.Collections.Generic;
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
                droneClient.Hover();
                loop.Wait();
            }
            catch(Exception ex)
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
                loop=Task.Run(()=>Loop(cancellationTokenSource.Token), cancellationTokenSource.Token);
            }
        }

        void Loop(CancellationToken token)
        {
            step = 0;
            while (!token.IsCancellationRequested)
            {
                switch (currentMission)
                {
                    case Missions.Objective:
                    case Missions.Home:
                        switch(step)
                        {
                            case 0:
                                flyToObjective();
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
                try
                {
                    Task.Delay(10).Wait();
                }
                catch (TaskCanceledException)
                {
                    //ok, do nothing
                }
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
            set { Set(ref currentCommand , value); }
        }

        int step;

        void flyToObjective()
        {
            var state = droneClient.NavigationData.State;
            if (state.HasFlag(NavigationState.Emergency))
                return;

            if (state.HasFlag(NavigationState.Landed))
            {
                Logger.LogInfo("takeof");

                droneClient.Takeoff();
                return;
            }
            if (analyzer.Detected)
            {
                var width = analyzer.FovSize.Width/2f;
                var change = width - analyzer.Center.X;
                if (change > 25)
                {
                    droneClient.Progress(FlightMode.Progressive,0, 0.02f);
                    Logger.LogInfo("left");
                }
                else if (change < -25)
                {
                    droneClient.Progress(FlightMode.Progressive,0, -0.02f);
                    Logger.LogInfo("right");
                }
                else
                {
                    droneClient.Hover();
                }

            }
            else
            {
                droneClient.Progress(FlightMode.Progressive,0, 0.02f);
            }
            
        }
    }
}
