using AR.Drone.Avionics.Objectives;
using AR.Drone.Avionics.Objectives.IntentObtainers;
using AR.Drone.Client;
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


        public DroneControls(DroneClient client)
        {
            InitializeComponent();
            droneClient = client;
            droneClient.NavigationDataAcquired += DroneClient_NavigationDataAcquired;
            autopilot = new AR.Drone.Avionics.Autopilot(client);
            autopilot.OnOutOfObjectives += Autopilot_OnOutOfObjectives;
            autopilot.BindToClient();
            autopilot.Start();
        }

        protected override void OnActivated(EventArgs e)
        {
            if (autopilot == null)
            {
                autopilot = new AR.Drone.Avionics.Autopilot(droneClient);
                autopilot.OnOutOfObjectives += Autopilot_OnOutOfObjectives;
                autopilot.BindToClient();
                autopilot.Start();
            }

            base.OnActivated(e);
        }

        /// <summary>
        /// Do not close, just hide, will be closed with main window
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (autopilot != null)
            {
                autopilot.Stop();
                autopilot.OnOutOfObjectives -= Autopilot_OnOutOfObjectives;
                autopilot.UnbindFromClient();
                autopilot = null;
            }
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
        }

        private void Autopilot_OnOutOfObjectives()
        {
            autopilot.Active = false;
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
        AR.Drone.Avionics.Autopilot autopilot;
        private void FlyToObjective_Click(object sender, RoutedEventArgs e)
        {
            if (!droneClient.IsActive)
                return;

            autopilot.ClearObjectives();
            autopilot.EnqueueObjective(new FlatTrim(1000));
            autopilot.EnqueueObjective(new Takeoff(3500));

            // One could use hover, but the method below, allows to gain/lose/maintain desired altitude
            autopilot.EnqueueObjective(
                Objective.Create(9500,
                    new VelocityX(0.4f),
                    new VelocityY(0.0f),
                    new Altitude(1.0f)
                )
            );

            autopilot.EnqueueObjective(new Land(5000));

            autopilot.Active = true;

        }

        private void Land_Click(object sender, RoutedEventArgs e)
        {
            autopilot.SetObjective(new Land(5000));
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            if (autopilot.Active)
            {
                autopilot.SetObjective(new Emergency());
            }
            else
            {
                droneClient.Emergency();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (autopilot.Active)
            {
                autopilot.SetObjective(new EmergencyReset());
            }
            else
            {
                droneClient.ResetEmergency();
            }
        }
    }
}
