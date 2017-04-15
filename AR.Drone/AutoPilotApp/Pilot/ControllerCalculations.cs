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
        Lagrange lagrangeDistance;
        Lagrange lagrangeDiff;
        Size screenSize;
        public ControllerCalculations(Size screenSize)
        {
            this.screenSize = screenSize;
            lagrangeDistance = new Pilot.Lagrange(
                new[] { 400d, 250d, 150d, 100d },
                new[] { 50d, 100d, 200d, 300d });
            lagrangeDiff = new Lagrange(
                new[] { 50d, 100d, 200d, 300d },
                new[] { 5d, 15d, 25d, 50d });
        }

        public double GetDistance(Size modelObject)
        {
            var s = modelObject.Width > modelObject.Height ? modelObject.Width : modelObject.Height;
            var v =s==0?double.MaxValue:  (double) s* (480d / screenSize.Height);
            return lagrangeDistance.InterpolateX(v);
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
