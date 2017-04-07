﻿using AForge.Video.DirectShow;
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

namespace AutoPilotApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Config config;
        Bitmaps bitmaps;
        private readonly DroneClient _droneClient;
        private readonly VideoPacketDecoderWorker _videoPacketDecoderWorker;

        bool first = true;
        protected override void OnActivated(EventArgs e)
        {
            if (first)
            {
                first = false;
                SimulatorButton_Click(this, null);
            }
            base.OnActivated(e);
        }

        public MainWindow()
        {
            InitializeComponent();
            var configObj = Application.Current.Resources["Config"];
            config = configObj as Config;
            if (config != null)
            {
                config.RedConfig.LowH = 141;
                config.RedConfig.HighH = 255;
                config.RedConfig.LowS = 56;
                config.RedConfig.HighS = 130;
                config.RedConfig.LowV = 145;
                config.RedConfig.HighV = 255;
            }
            configObj = Application.Current.Resources["Bitmaps"];
            bitmaps = configObj as Bitmaps;
            this.DataContextChanged += (o, e) => {
                System.Diagnostics.Debug.WriteLine(e.Property.Name);
            };
            Closed += MainWindow_Closed;

            _droneClient = new DroneClient("192.168.2.1");
            _droneClient.VideoPacketAcquired += OnVideoPacketAcquired;

            _videoPacketDecoderWorker = new VideoPacketDecoderWorker(AR.Drone.Video.PixelFormat.BGR24, true, OnVideoPacketDecoded);
            _videoPacketDecoderWorker.Start();

            frameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(20), DispatcherPriority.Normal, timerElapsed, this.Dispatcher);
        }

        private void timerElapsed(object sender, EventArgs e)
        {
            if (_frame != null)
            {
                bitmaps.Bitmap = VideoHelper.CreateBitmap(ref _frame);
                analyze(bitmaps.Bitmap);
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
            if (_droneClient != null)
            {
                _droneClient.Stop();
                _droneClient.Dispose();
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
                catch { }

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

        BitmapSource ConvertBitmap(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                bitmapimage.Freeze();
                return new WriteableBitmap(bitmapimage);
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

        public ColorConfig currentConfig
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

        private void analyze(System.Drawing.Bitmap bitmap)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(bitmap))
            {

                UMat uimage = new UMat();

                CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Hsv);


                double cannyThreshold = 180.0;
                double cannyThresholdLinking = 120.0;
                UMat cannyEdges = new UMat();
                CvInvoke.Canny(img, cannyEdges, cannyThreshold, cannyThresholdLinking);
                LineSegment2D[] lines = CvInvoke.HoughLinesP(
                   cannyEdges,
                   1, //Distance resolution in pixel-related units
                   Math.PI / 45.0, //Angle resolution measured in radians.
                   20, //threshold
                   30, //min Line width
                   10); //gap between lines

                foreach (LineSegment2D line in lines)
                    img.Draw(line, new Bgr(System.Drawing.Color.Green), 2);

                //use image pyr to remove noise
                UMat pyrDown = new UMat();
                CvInvoke.PyrDown(uimage, pyrDown);
                CvInvoke.PyrUp(pyrDown, uimage);

                UMat imgThresholded = new UMat();
                MCvScalar lower = new MCvScalar(currentConfig.LowH, currentConfig.LowS, currentConfig.LowV);
                MCvScalar upper = new MCvScalar(currentConfig.HighH, currentConfig.HighS, currentConfig.HighV);

                CvInvoke.InRange(uimage, new ScalarArray(lower), new ScalarArray(upper), imgThresholded);

                //Calculate the moments of the thresholded image
                var oMoments = CvInvoke.Moments(imgThresholded);

                double dM01 = oMoments.M01;
                double dM10 = oMoments.M10;
                double dArea = oMoments.M00;

                // if the area <= 10000, I consider that the there are no object in the image and it's because of the noise, the area is not zero 
                if (dArea > 10000)
                {
                    //calculate the position of the ball
                    int posX = (int)(dM10 / dArea);
                    int posY = (int)(dM01 / dArea);
                    CvInvoke.Circle(img, new System.Drawing.Point(posX, posY), (int)(dArea / 100000d), new MCvScalar(255, 0, 0), -1);
                }

                bitmaps.Calculations = sw.ElapsedMilliseconds;
                sw.Restart();
                bitmaps.Bitmap = bitmap;
                bitmaps.UpdateImages(bitmap,
                    img.ToBitmap(),
                    uimage.Bitmap,
                    imgThresholded.Bitmap);

                bitmaps.ImageSet = sw.ElapsedMilliseconds;
                bitmaps.FPS = bitmaps.Calculations + bitmaps.ImageSet;
            }
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
                using (StreamWriter w = new StreamWriter(dlg.FileName))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(w))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(jw, config, typeof(Config));
                    }

                }
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
                using (StreamReader w = new StreamReader(dlg.FileName))
                {
                    using (JsonTextReader jw = new JsonTextReader(w))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        var newConfig = serializer.Deserialize<Config>(jw);
                        if (newConfig != null)
                        {
                            Application.Current.Resources["Config"] = newConfig;
                            config = newConfig;
                        }
                    }
                }
            }
        }

        private void StreamButton_Click(object sender, RoutedEventArgs e)
        {
            _droneClient.Start();
        }
    }
}
