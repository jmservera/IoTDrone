using AutoPilotApp.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            bool useMorphologic=true;
            bool detectBox = false;
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            bitmaps.UpdateImages(bitmap);
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(bitmaps.Bitmap))
            {
                UMat uimage = new UMat();
                CvInvoke.CvtColor(img.Clone(), uimage, ColorConversion.Bgr2Hsv,3);

                double cannyThreshold = 180.0;
                double cannyThresholdLinking = 10.0;
                UMat cannyEdges = new UMat();
                CvInvoke.Canny(img.Clone(), cannyEdges, cannyThreshold, cannyThresholdLinking);

                if (detectBox)
                {
                    var boxList = BoxDetection(cannyEdges);
                    foreach (var box in boxList)
                    {
                        for (int i = 0; i < box.Length; i++)
                        {
                            var pointA = box[i];
                            var pointB = box[(i + 1) % box.Length];
                            CvInvoke.Line(img, pointA, pointB, new Bgr(Color.DarkOrange).MCvScalar, 2);
                        }
                        //CvInvoke.Polylines(img, box, true, new Bgr(Color.DarkOrange).MCvScalar, 2);
                    }
                }

                //use image pyr to remove noise
                UMat pyrDown = new UMat();
                CvInvoke.PyrDown(uimage, pyrDown);
                CvInvoke.PyrUp(pyrDown, uimage);

                UMat imgThresholded = new UMat();
                MCvScalar lower = new MCvScalar(currentConfig.LowH, currentConfig.LowS, currentConfig.LowV);
                MCvScalar upper = new MCvScalar(currentConfig.HighH, currentConfig.HighS, currentConfig.HighV);

                CvInvoke.InRange(uimage, new ScalarArray(lower), new ScalarArray(upper), imgThresholded);

                if (useMorphologic)
                {
                    morphOps(imgThresholded);
                }

                ////Calculate the moments of the thresholded image
                //var oMoments = CvInvoke.Moments(imgThresholded);

                //double dM01 = oMoments.M01;
                //double dM10 = oMoments.M10;
                //double dArea = oMoments.M00;

                //// if the area <= 10000, I consider that the there are no object in the image and it's because of the noise, the area is not zero 
                //if (dArea > 10000)
                //{
                //    //calculate the position of the ball
                //    int posX = (int)(dM10 / dArea);
                //    int posY = (int)(dM01 / dArea);
                //    CvInvoke.Circle(img, new System.Drawing.Point(posX, posY), (int)(dArea / 100000d),
                //        new Bgr(Color.DarkOrange).MCvScalar, 4);
                //}


                var contours = ContourDetection(imgThresholded);
                foreach (var box in contours)
                {
                    for (int i = 0; i < box.Length; i++)
                    {
                        var pointA = box[i];
                        var pointB = box[(i + 1) % box.Length];
                        CvInvoke.Line(img, pointA, pointB, new Bgr(Color.DarkOrange).MCvScalar, 4);
                    }
                }

                    bitmaps.Calculations = sw.ElapsedMilliseconds;
                sw.Restart();
                bitmaps.UpdateImages(null, img.ToBitmap(),
                    cannyEdges.Bitmap,
                    imgThresholded.Bitmap);

                bitmaps.ImageSet = sw.ElapsedMilliseconds;
                bitmaps.FPS = bitmaps.Calculations + bitmaps.ImageSet;
            }
        }

        private void morphOps(UMat imgThresholded)
        {

            Mat erodeElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            Mat dilateElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(8, 8), new Point(-1, -1));

            CvInvoke.Erode(imgThresholded, imgThresholded, erodeElement, new Point(-1, -1), 2, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.Dilate(imgThresholded, imgThresholded, dilateElement, new Point(-1, -1), 2, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
        }

        private List<Point[]> ContourDetection(UMat thresholded)
        {
            //CvInvoke.Dilate(cannyEdges, cannyEdges, null,new Point(-1,-1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            List<Point[]> boxList = new List<Point[]>(); //a box is a rotated rectangle

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(thresholded.Clone(), contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (CvInvoke.ContourArea(approxContour, false) > 250) //only consider contours with area greater than 250
                        {
                            boxList.Add(contour.ToArray());// CvInvoke.MinAreaRect(approxContour));
                        }
                    }
                }
            }
            return boxList;
        }
        private List<Point[]> BoxDetection(UMat cannyEdges)
        {
            //CvInvoke.Dilate(cannyEdges, cannyEdges, null,new Point(-1,-1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            List<Point[]> boxList = new List<Point[]>(); //a box is a rotated rectangle

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges.Clone(), contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        if (CvInvoke.ContourArea(approxContour, false) > 250) //only consider contours with area greater than 250
                        {
                            if (approxContour.Size ==4) //The contour has 4 vertices.
                            {
                                #region determine if all the angles in the contour are within [80, 100] degree
                                bool isRectangle = true;
                                Point[] pts = approxContour.ToArray();
                                LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                for (int j = 0; j < edges.Length; j++)
                                {
                                    double angle = Math.Abs(
                                       edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));
                                    if (angle < 40 || angle > 130)
                                    {
                                        isRectangle = false;
                                        break;
                                    }
                                }
                                #endregion

                                if (isRectangle) boxList.Add(approxContour.ToArray());// CvInvoke.MinAreaRect(approxContour));

                            }
                        }
                    }
                }
            }
            return boxList;
        }
    }
}
