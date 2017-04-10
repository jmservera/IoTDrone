using AR.Drone.Avionics;
using AR.Drone.Avionics.Objectives;
using AR.Drone.Client;
using AR.Drone.Client.Command;
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
        AimCoords aimCoords;
        DroneClient droneClient;
        private Missions currentMission;

        public Missions CurrentMission
        {
            get { return currentMission; }
        }

        Autopilot autoPilot;
        public Controller(DroneClient client)
        {
            droneClient = client;
            autoPilot = new Autopilot(client);
        }

        public void EmergencyStop()
        {
            droneClient.Emergency();
            stopAutopilot();
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

        public void Start(Missions mission, AimCoords coords)
        {
            stopAutopilot();

            currentMission = mission;
            aimCoords = coords;

            RaisePropertyChanged(nameof(CurrentMission));

            startAutopilot();

        }

        CancellationTokenSource cancellationTokenSource;
        void startAutopilot()
        {
            if (cancellationTokenSource != null)
                return;
            lock (this)
            {
                if (cancellationTokenSource != null)
                    return;

                cancellationTokenSource = new CancellationTokenSource();
                Task.Run(()=>Loop(cancellationTokenSource.Token), cancellationTokenSource.Token);
            }
        }

        void Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                switch (currentMission)
                {
                    case Missions.Objective:
                    case Missions.Home:
                        step = 0;
                        flyToObjective();
                        break;
                    case Missions.AttendeesPicture:
                    default:
                        {
                            CurrentCommand = "Hover";
                            droneClient.Hover();
                            break;
                        }
                }
                Task.Delay(10, token).Wait();
            }
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
            
        }
    }
}
