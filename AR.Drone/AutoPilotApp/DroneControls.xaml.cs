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
            set { Set(ref battery ,value); }
        }




        public DroneControls(DroneClient client)
        {
            InitializeComponent();
            droneClient = client;
            droneClient.NavigationDataAcquired += DroneClient_NavigationDataAcquired;
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
    }
}
