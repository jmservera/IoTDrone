using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.Win32;
using AR.Drone.Client;
using AR.Drone.Data;
using AR.Drone.Video;
using AR.Drone.WinApp;
using System.Windows.Threading;
using Emgu.CV.Util;
using AutoPilotApp.Models;
using System.ComponentModel;
using AutoPilotApp.CV;
using AutoPilotApp.IoT;
using AutoPilotApp.Common;
using System.Configuration;

namespace AutoPilotApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pilot.Controller autoPilot;
        Config config;
        ColorConfig currentConfig
        {
            get
            {
                if (!config.Direction)
                {
                    return config.RedConfig;
                }
                else
                {
                    return config.GreenConfig;
                }
            }
        }

        Bitmaps bitmaps;
        Analyzer analyzer;
        AnalyzerOutput analyzerOutput;
        CognitiveData cognitiveData;
        CognitiveController cognitiveController;
        IoTHubController iotController;
        DroneClient droneClient;


        VideoPacketDecoderWorker _videoPacketDecoderWorker;

        bool first = true;
        protected override void OnActivated(EventArgs e)
        {
            if (first)
            {
                first = false;
                // SimulatorButton_Click(this, null);
                collapseColors_Click(this, null);
            }
            base.OnActivated(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (controlsWindow!=null)
            {
                controlsWindow.Close();
            }
            base.OnClosing(e);
        }

        public MainWindow()
        {
            InitializeComponent();

            Logger.LogReceived += log;

            var configObj = Application.Current.Resources["Config"];
            updateConfig(configObj as Config);

            var bmpsObj = Application.Current.Resources["Bitmaps"];
            bitmaps = bmpsObj as Bitmaps;

            var analyzerOutObj = Application.Current.Resources["AnalyzerOuput"];
            analyzerOutput = analyzerOutObj as AnalyzerOutput;
            var useGPUObj= ConfigurationManager.AppSettings["UseGPU"];
            bool useGPU = false;
            if (useGPUObj!=null)
            {
                bool.TryParse(useGPUObj.ToString(), out useGPU);
            }
            analyzer = new Analyzer(bitmaps, analyzerOutput, useGPU);



            var cogObj = Application.Current.Resources["CognitiveData"];
            cognitiveData = cogObj as CognitiveData;
            cognitiveController = new CognitiveController(bitmaps, cognitiveData);


            this.DataContextChanged += (o, e) => {
                System.Diagnostics.Debug.WriteLine(e.Property.Name);
            };
            Closed += MainWindow_Closed;

            configDrone();

            autoPilot = new Pilot.Controller(droneClient, analyzerOutput, config);
            iotController = new IoTHubController(droneClient,analyzerOutput);

            frameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(20), DispatcherPriority.Normal, timerElapsed, this.Dispatcher);
        }

        async void log(object sender, LoggerEventArgs eventArgs)
        {
            //if (eventArgs.Level != LogLevel.Info)
            {
                var dt = DateTime.Now;
                await logger.Dispatcher.InvokeAsync( () =>
                {
                    logger.Text = dt.ToLongTimeString() +" "+ eventArgs.Message + Environment.NewLine + logger.Text;
                    if (logger.Text.Length > 1500)
                    {
                        logger.Text.Substring(0, 1000);
                    }
                    if (eventArgs.Level == LogLevel.Event)
                    {
                        infoLog.Text = eventArgs.Message + Environment.NewLine + infoLog.Text;
                    }
                });
            }
        }


        void configDrone()
        {
            if (droneClient != null)
            {
                droneClient.VideoPacketAcquired -= OnVideoPacketAcquired;
                droneClient.Dispose();
            }
            Logger.LogInfo($"Configuring Drone at {config.DroneIP}");
            droneClient = new DroneClient(config.DroneIP);
            droneClient.VideoPacketAcquired += OnVideoPacketAcquired;

            if (_videoPacketDecoderWorker != null)
            {
                _videoPacketDecoderWorker.Stop();
                _videoPacketDecoderWorker.Dispose();
            }
            _videoPacketDecoderWorker = new VideoPacketDecoderWorker(AR.Drone.Video.PixelFormat.BGR24, true, OnVideoPacketDecoded);
            _videoPacketDecoderWorker.Start();
        }

        void timerElapsed(object sender, EventArgs e)
        {
            if (_frame != null)
            {
                analyze(VideoHelper.CreateBitmap(ref _frame));
            }
        }

        DispatcherTimer frameTimer;
        VideoFrame _frame;
        private void OnVideoPacketDecoded(VideoFrame frame)
        {
            _frame = frame;
        }

        private void OnVideoPacketAcquired(VideoPacket packet)
        {
            if (_videoPacketDecoderWorker.IsAlive)
                _videoPacketDecoderWorker.EnqueuePacket(packet);
        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            if (frameTimer != null)
            {
                frameTimer.Stop();
            }
            if (droneClient != null)
            {
                droneClient.Stop();
                droneClient.Dispose();
            }
            if (_videoPacketDecoderWorker != null)
            {
                _videoPacketDecoderWorker.Stop();
                _videoPacketDecoderWorker.Dispose();
            }
            await closeSource();
        }

        VideoCaptureDevice videoSource;

        private async void SimulatorButton_Click(object sender, RoutedEventArgs e)
        {
            await closeSource();
            //List all available video sources. (That can be webcams as well as tv cards, etc)
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Check if atleast one video source is available
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                        {
                            if (videoSource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                        }
                        //Set the highest resolution as active
                        videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                videoSource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);

                //Start recording
                videoSource.Start();
            }
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            var action = new Action(() => analyze((Bitmap)eventArgs.Frame.Clone()));
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private async Task closeSource()
        {
            if (videoSource != null)
            {
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                }
                while (videoSource.IsRunning)
                    await Task.Delay(100);
                videoSource = null;
            }
        }

        private void analyze(System.Drawing.Bitmap bitmap)
        {
            analyzer.Analyze(bitmap, currentConfig);
        }

        private void Original_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                var input = (System.Windows.Controls.Image)sender;
                var w = input.ActualWidth;
                var h = input.ActualHeight;
                var position = e.GetPosition((IInputElement)sender);
                var bmpSize = bitmaps.Bitmap.Size;

                var x = position.X * bmpSize.Width / w;
                var y = position.Y * bmpSize.Height / h;
                var color = bitmaps.Bitmap.GetPixel((int)x, (int)y);
                UMat uimage = new UMat();
                using (Bitmap bmp = new Bitmap(1, 1))
                {
                    bmp.SetPixel(0, 0, color);
                    Image<Bgr, byte> img = new Image<Bgr, byte>(bmp);
                    var pix = new Image<Hsv, byte>(new System.Drawing.Size(1, 1));
                    CvInvoke.CvtColor(img, pix, ColorConversion.Bgr2Hsv);
                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        currentConfig.HighH = pix.Data[0, 0, 0];
                        currentConfig.HighS = pix.Data[0, 0, 1];
                        currentConfig.HighV = pix.Data[0, 0, 2];
                    }
                    else if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        currentConfig.LowH = pix.Data[0, 0, 0];
                        currentConfig.LowS = pix.Data[0, 0, 1];
                        currentConfig.LowV = pix.Data[0, 0, 2];
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "config";
            dlg.DefaultExt = ".json";
            dlg.Filter = "Config Files (.json)|*.json";
            if (dlg.ShowDialog() ?? false)
            {
                config.Save(dlg.FileName);
            }
        }



        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.FileName = "config";
            dlg.DefaultExt = ".json";
            dlg.Filter = "Config Files (.json)|*.json";
            if (dlg.ShowDialog() ?? false)
            {
                using (StreamReader r = new StreamReader(dlg.FileName))
                {
                    Config newConfig = Config.Load(r);

                    if (newConfig != null)
                    {
                        Application.Current.Resources["Config"] = newConfig;
                        updateConfig(newConfig);
                    }
                }
            }
        }

        private void updateConfig(Config newConfig)
        {
            if (config!=null)
            {
                config.PropertyChanged -= configChanged;
            }
            config = newConfig;
            if (newConfig != null)
            {
                config.PropertyChanged += configChanged;
            }
        }

        private void configChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Config.DroneIP))
            {
                configDrone();
            }
        }

        private void StreamButton_Click(object sender, RoutedEventArgs e)
        {
            droneClient.Start();
        }

        private void collapseColors_Click(object sender, RoutedEventArgs e)
        {
            // ▼
            if((string)collapseColors.Content== "▼")
            {
                collapseColors.Content = "▲";
                colorsGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                collapseColors.Content = "▼";
                colorsGrid.Visibility = Visibility.Visible;
            }
        }

        DroneControls controlsWindow;

        private void DroneControls_Click(object sender, RoutedEventArgs e)
        {
            if (controlsWindow == null)
            {
                controlsWindow = new DroneControls(droneClient, analyzerOutput, config, autoPilot);
                controlsWindow.Owner = this;
            }
            controlsWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            logger.Text = String.Empty;
        }
    }
}
