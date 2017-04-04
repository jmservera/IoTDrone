using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Client.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AR.Drone.WinApp
{
    public partial class Control : Form
    {
        DroneClient _droneClient;
        private Settings _settings;

        public Control(DroneClient droneClient)
        {
            InitializeComponent();
            _droneClient = droneClient;
        }

        private void btnFlatTrim_Click(object sender, EventArgs e)
        {
            _droneClient.FlatTrim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _droneClient.Takeoff();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _droneClient.Land();
        }


        private void btnSwitchCam_Click(object sender, EventArgs e)
        {
            var configuration = new Settings();
            configuration.Video.Channel = VideoChannelType.Next;
            _droneClient.Send(configuration);
        }

        private void btnHover_Click(object sender, EventArgs e)
        {
            _droneClient.Hover();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, gaz: 0.25f);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, gaz: -0.25f);
        }

        private void btnTurnLeft_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: 0.25f);
        }

        private void btnTurnRight_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, yaw: -0.25f);
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, roll: -0.05f);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, roll: 0.05f);
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, pitch: -0.05f);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _droneClient.Progress(FlightMode.Progressive, pitch: 0.05f);
        }

        private void btnReadConfig_Click(object sender, EventArgs e)
        {
            Task<Settings> configurationTask = _droneClient.GetConfigurationTask();
            configurationTask.ContinueWith(delegate (Task<Settings> task)
            {
                if (task.Exception != null)
                {
                    Trace.TraceWarning("Get configuration task is faulted with exception: {0}", task.Exception.InnerException.Message);
                    return;
                }

                _settings = task.Result;
            });
            configurationTask.Start();
        }

        private void btnSendConfig_Click(object sender, EventArgs e)
        {
            var sendConfigTask = new Task(() =>
            {
                if (_settings == null) _settings = new Settings();
                Settings settings = _settings;

                if (string.IsNullOrEmpty(settings.Custom.SessionId) ||
                    settings.Custom.SessionId == "00000000")
                {
                    // set new session, application and profile
                    _droneClient.AckControlAndWaitForConfirmation(); // wait for the control confirmation

                    settings.Custom.SessionId = Settings.NewId();
                    _droneClient.Send(settings);

                    _droneClient.AckControlAndWaitForConfirmation();

                    settings.Custom.ProfileId = Settings.NewId();
                    _droneClient.Send(settings);

                    _droneClient.AckControlAndWaitForConfirmation();

                    settings.Custom.ApplicationId = Settings.NewId();
                    _droneClient.Send(settings);

                    _droneClient.AckControlAndWaitForConfirmation();
                }

                settings.General.NavdataDemo = false;
                settings.General.NavdataOptions = NavdataOptions.All;

                settings.Video.BitrateCtrlMode = VideoBitrateControlMode.Dynamic;
                settings.Video.Bitrate = 1000;
                settings.Video.MaxBitrate = 2000;

                //settings.Leds.LedAnimation = new LedAnimation(LedAnimationType.BlinkGreenRed, 2.0f, 2);
                //settings.Control.FlightAnimation = new FlightAnimation(FlightAnimationType.Wave);

                // record video to usb
                //settings.Video.OnUsb = true;
                // usage of MP4_360P_H264_720P codec is a requirement for video recording to usb
                //settings.Video.Codec = VideoCodecType.MP4_360P_H264_720P;
                // start
                //settings.Userbox.Command = new UserboxCommand(UserboxCommandType.Start);
                // stop
                //settings.Userbox.Command = new UserboxCommand(UserboxCommandType.Stop);


                //send all changes in one pice
                _droneClient.Send(settings);
            });
            sendConfigTask.Start();
        }
    }
}
