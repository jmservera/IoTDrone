using AR.Drone.Avionics.Objectives;
using AR.Drone.Avionics.Objectives.IntentObtainers;
using AR.Drone.Client;
using AR.Drone.Client.Command;
using AutoPilotApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoPilotApp
{
    /// <summary>
    /// Interaction logic for DroneControls.xaml
    /// </summary>
    public partial class DroneControls : Window, INotifyPropertyChanged
    {
        DroneClient droneClient;
        Pilot.Controller droneController;

        private float altitude;

        public float Altitude
        {
            get { return altitude; }
            set { Set(ref altitude, value); }
        }

        private float battery;

        public float Battery
        {
            get { return battery; }
            set { Set(ref battery, value); }
        }

        AnalyzerOutput analyzerOutput;
        Config config;

        public DroneControls(DroneClient client, AnalyzerOutput output, Config configuration, Pilot.Controller autopilot)
        {
            InitializeComponent();
            analyzerOutput = output;
            config = configuration;
            droneClient = client;
            droneClient.NavigationDataAcquired += DroneClient_NavigationDataAcquired;
            droneController = autopilot;

            this.KeyDown += DroneControls_KeyDown;
        }
        

        private void DroneControls_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Key);
            switch (e.Key)
            {
                case Key.Left:
                    droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
                    break;
                case Key.Up:
                    droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
                    break;
                case Key.Right:
                    droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
                    break;
                case Key.Down:
                    droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
                    break;
                case Key.A:
                    droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
                    break;
                case Key.D:
                    droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
                    break;
                case Key.S:
                    droneClient.Progress(FlightMode.Progressive, pitch: 0.05f);
                    break;
                case Key.W:
                    droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
                    break;
                case Key.I:
                    droneClient.Takeoff();
                    break;
                case Key.O:
                    droneClient.Land();
                    break;
                default:
                    break;
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        /// <summary>
        /// Do not close, just hide, will be closed with main window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
        }

        private void DroneClient_NavigationDataAcquired(AR.Drone.Data.Navigation.NavigationData obj)
        {
            Altitude = obj.Altitude;
            Battery = obj.Battery.Percentage;
        }

        private void Activate_Click(object sender, RoutedEventArgs e)
        {
            droneClient.Start();
        }
        private void FlyToObjective_Click(object sender, RoutedEventArgs e)
        {
            if (!droneClient.IsActive)
                return;
            if (droneController != null)
            {
                droneController.Stop();
                droneController = null;
            }
            else
            {
                droneController = new Pilot.Controller(droneClient, analyzerOutput, config);
                droneController.Start(Pilot.Missions.Objective);
            }

        }

        private void Land_Click(object sender, RoutedEventArgs e)
        {
            if (droneController != null)
            {
                droneController.Stop();
            }
            droneClient.Land();
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            if (droneController != null)
            {
                droneController.Stop();
            }
            droneClient.Emergency();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (droneController != null)
            {
                droneController.Stop();
            }
            droneClient.ResetEmergency();
        }

        private void FlatTrim_Click(object sender, RoutedEventArgs e)
        {
            droneClient.FlatTrim();
        }
    }
}
