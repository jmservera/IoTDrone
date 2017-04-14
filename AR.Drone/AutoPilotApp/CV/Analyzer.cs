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
        bool useGPU = true;
        private AnalyzerOuput analyzerOutput;
        private bool useGPU1;

        public Analyzer(Bitmaps input, AnalyzerOuput output, bool useGPU=true)
        {
            analyzerOutput = output;
            bitmaps = input;
            this.useGPU = useGPU;
        }


        private IImage createMat()
        {
            return useGPU? (IImage)new UMat(): new Mat();
        }

        public void Analyze(System.Drawing.Bitmap bitmap, ColorConfig currentConfig, bool useMorphologic = true, bool detectBox = false)
        {
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            bitmaps.UpdateImages(bitmap);
            using (Image<Bgr, byte> img = new Image<Bgr, byte>(bitmaps.Bitmap))
            {
                IImage uimage = createMat();
                CvInvoke.CvtColor(img.Clone(), uimage, ColorConversion.Bgr2Hsv, 3);

                double cannyThreshold = 180.0;
                double cannyThresholdLinking = 10.0;
                IImage cannyEdges = createMat();
                CvInvoke.Canny(img.Clone(), cannyEdges, cannyThreshold, cannyThresholdLinking);

                if (detectBox)
                {
                    var boxList = BoxDetection(cannyEdges);
                    foreach (var box in boxList)
                    {
                        for (int i = 0; i < box.Item1.Length; i++)
                        {
                            var pointA = box.Item1[i];
                            var pointB = box.Item1[(i + 1) % box.Item1.Length];
                            CvInvoke.Line(img, pointA, pointB, new Bgr(Color.DarkOrange).MCvScalar, 2);
                        }
                    }
                }


                //use image pyr to remove noise
                IImage pyrDown = createMat();
                CvInvoke.PyrDown(uimage, pyrDown);
                CvInvoke.PyrUp(pyrDown, uimage);

                IImage imgThresholded = createMat();
                MCvScalar lower = new MCvScalar(currentConfig.LowH, currentConfig.LowS, currentConfig.LowV);
                MCvScalar upper = new MCvScalar(currentConfig.HighH, currentConfig.HighS, currentConfig.HighV);

                CvInvoke.InRange(uimage, new ScalarArray(lower), new ScalarArray(upper), imgThresholded);

                if (useMorphologic)
                {
                    morphOps(imgThresholded);
                }

                var contours = ContourDetection(imgThresholded);
                var maxContour = contours.OrderByDescending(t => t.Item2).FirstOrDefault();
                foreach (var box in contours)
                {
                    MCvScalar contourColor;
                    if (box == maxContour)
                    {
                        contourColor = new Bgr(Color.DarkOrange).MCvScalar;
                        var vertices = box.Item4.GetVertices();
                        for(int i = 0; i < 4; i++)
                        {
                            CvInvoke.Line(img, new Point((int) vertices[i].X,(int)vertices[i].Y),
                                new Point((int)vertices[(i + 1) % 4].X, (int)vertices[(i + 1) % 4].Y), contourColor);
                        }

                    }
                    else
                    {
                        contourColor = new Bgr(Color.Green).MCvScalar;
                    }
                    for (int i = 0; i < box.Item1.Length; i++)
                    {
                        var pointA = box.Item1[i];
                        var pointB = box.Item1[(i + 1) % box.Item1.Length];
                        CvInvoke.Line(img, pointA, pointB, contourColor, 4);
                    }
                    CvInvoke.Circle(img, box.Item3,5,contourColor,2);
                }

                bitmaps.Calculations = sw.ElapsedMilliseconds;
                sw.Restart();
                bitmaps.UpdateImages(null, img.ToBitmap(),
                    cannyEdges.Bitmap,
                    imgThresholded.Bitmap);

                bitmaps.ImageSet = sw.ElapsedMilliseconds;
                bitmaps.FPS = bitmaps.Calculations + bitmaps.ImageSet;

                if (this.analyzerOutput != null)
                {
                    analyzerOutput.FovSize = bitmaps.Bitmap.Size;
                    if (maxContour != null)
                    {
                        analyzerOutput.Detected = true;
                        analyzerOutput.Center = maxContour.Item3;
                        analyzerOutput.Distance = maxContour.Item2;
                        analyzerOutput.Width = (int) maxContour.Item4.Size.Width;
                        analyzerOutput.Height = (int)maxContour.Item4.Size.Height;
                    }
                    else
                    {
                        analyzerOutput.Detected = false;
                        analyzerOutput.Center = Point.Empty;
                        analyzerOutput.Distance = 0;
                    }
                }
            }
        }

        private void morphOps(IImage imgThresholded)
        {
            Mat erodeElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            Mat dilateElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(8, 8), new Point(-1, -1));

            CvInvoke.Erode(imgThresholded, imgThresholded, erodeElement, new Point(-1, -1), 2, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.Dilate(imgThresholded, imgThresholded, dilateElement, new Point(-1, -1), 2, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
        }

        private List<Tuple<Point[], double, Point, RotatedRect>> ContourDetection(IImage thresholded)
        {
            var boxList = new List<Tuple<Point[], double, Point, RotatedRect>>(); //a box is a rotated rectangle

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours((IImage) ((ICloneable)thresholded).Clone(), contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        //CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);

                        var area = CvInvoke.ContourArea(contour, false);
                        if (area > 250) //only consider contours with area greater than 250
                        {
                            var moments = CvInvoke.Moments(contour);
                            var cX = moments.M10 / moments.M00;
                            var cY = moments.M01 / moments.M00;
                            var rect = CvInvoke.MinAreaRect(contour);
                            boxList.Add(new Tuple<Point[], double, Point, RotatedRect>( contour.ToArray(),area, new Point((int)cX, (int)cY), rect));// CvInvoke.MinAreaRect(approxContour));
                        }
                    }
                }
            }
            return boxList;
        }

        private List<Tuple<Point[],double>> BoxDetection(IImage cannyEdges)
        {
            List<Tuple<Point[], double>> boxList = new List<Tuple<Point[], double>>();

            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours((IImage)((ICloneable)cannyEdges).Clone(), contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    using (VectorOfPoint approxContour = new VectorOfPoint())
                    {
                        CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                        var area = CvInvoke.ContourArea(approxContour, false);
                        if (area > 250) //only consider contours with area greater than 250
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

                                if (isRectangle) boxList.Add( new Tuple<Point[], double>(approxContour.ToArray(),area));// CvInvoke.MinAreaRect(approxContour));

                            }
                        }
                    }
                }
            }
            return boxList;
        }
    }
}
