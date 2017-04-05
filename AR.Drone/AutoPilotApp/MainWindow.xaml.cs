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

namespace AutoPilotApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Config config;
        Bitmaps bitmaps;

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
        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            await closeSource();
        }

        VideoCaptureDevice videoSource;

        private async void Button_Click(object sender, RoutedEventArgs e)
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

        void setPic(Bitmap bitmap, Action<BitmapImage> setter)
        {
            try
            {
                BitmapImage bi = new BitmapImage();
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Bmp);
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                    bi.Freeze();
                }
                Dispatcher.BeginInvoke(new ThreadStart(delegate { setter(bi); }));
            }
            catch (Exception ex)
            {
                //catch your error here
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
            Image<Bgr, byte> img = new Image<Bgr, byte>(bitmap);

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

                var blank = img.CopyBlank();
                CvInvoke.Circle(img, new System.Drawing.Point(posX, posY), 20, new MCvScalar(255, 0, 0), -1);
                //img = img + blank;
            }

            bitmaps.Bitmap = bitmap;
            setPic(bitmap, (o) => bitmaps.Original = o);
            setPic(img.ToBitmap(), (o) => bitmaps.First = o);
            setPic(uimage.Bitmap, (o) => bitmaps.Second = o);
            setPic(imgThresholded.Bitmap, (o) => bitmaps.Final = o);
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
                        currentConfig.HighH= pix.Data[0, 0, 0];
                        currentConfig.HighS= pix.Data[0, 0, 1];
                        currentConfig.HighV= pix.Data[0, 0, 2];
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
    }
}
