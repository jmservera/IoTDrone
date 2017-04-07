using AutoPilotApp.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.CV
{
    public class Analyzer
    {
        Bitmaps bitmaps;
        public Analyzer(Bitmaps bitmaps)
        {
            this.bitmaps = bitmaps;
        }

        public void Analyze(System.Drawing.Bitmap bitmap, ColorConfig currentConfig)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            bitmaps.UpdateImages(bitmap);
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(bitmaps.Bitmap))
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
                    CvInvoke.Circle(img, new System.Drawing.Point(posX, posY), (int)(dArea / 100000d),
                        new MCvScalar(255, 0, 0), -1);
                }

                bitmaps.Calculations = sw.ElapsedMilliseconds;
                sw.Restart();
                bitmaps.UpdateImages(null, img.ToBitmap(),
                    cannyEdges?.Bitmap,
                    imgThresholded?.Bitmap);

                bitmaps.ImageSet = sw.ElapsedMilliseconds;
                bitmaps.FPS = bitmaps.Calculations + bitmaps.ImageSet;
            }
        }
    }
}
