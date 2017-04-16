using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Pilot
{
    public class ControllerCalculations
    {
        //Lagrange lagrangeDistance;
        Lagrange lagrangeDiff;
        Size screenSize;

        double a, b;

        public ControllerCalculations(Size screenSize)
        {
            this.screenSize = screenSize;
            //lagrangeDistance = new Pilot.Lagrange(
            //    new[] x { 180d, 120d, 75d, 60d },
            //    new[] y { 100d, 200d, 300d, 400d });
            a = (300d - 100d) / (75d - 180d);
            b = ((75d * 100d) - (180d * 300d)) / (75d - 180d); 
            lagrangeDiff = new Lagrange(
                new[] { 50d, 100d, 200d, 300d },
                new[] { 20d, 30d, 50d, 90d });
        }

        public double GetDistance(Size modelObject)
        {
            var s = modelObject.Width > modelObject.Height ? modelObject.Width : modelObject.Height;
            var v =s==0?double.MaxValue:  (double) s* (480d / screenSize.Height);
            return (a*v)+b;
        }

        public double GetDiff(double distance)
        {
            return lagrangeDiff.InterpolateX(distance);
        }
    }

    public class Lagrange
    {
        double[] xValues;
        double[] yValues;

        public Lagrange(double[] xValues, double[] yValues)
        {
            this.xValues = xValues;
            this.yValues = yValues;
        }

        public double InterpolateX(double x)
        {
            double result = 0;
            for (var i = 0; i < xValues.Length; i++)
            {
                var numerator = 1d;
                var denominator = 1d;
                for (var j = 0; j < xValues.Length; j++)
                {
                    if (j != i)
                    {
                        numerator *= (x - xValues[j]);
                        denominator *= (xValues[i] - xValues[j]);
                    }
                }
                result += yValues[i] * (numerator / denominator);
            }
            return result;
        }

    }
}
