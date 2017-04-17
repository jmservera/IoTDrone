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
        Size screenSize;
        double a, b, a1, b1;

        public ControllerCalculations(Size screenSize)
        {
            this.screenSize = screenSize;
            var x = new[] { 180d, 75d };
            var y = new[] { 100d, 300d };
            var m = calcLine(x, y);
            a = m.Item1;
            b = m.Item2;

            var xd = new[] { 50d, 200d };
            var yd = new[] { 20d, 50d };
            var d=calcLine(xd, yd);
            a1 = d.Item1;
            b1 = d.Item2;
        }

        static Tuple<double,double> calcLine(double[] x, double[] y)
        {
            var a = (y[1] - y[0]) / (x[1] - x[0]);
            var b = ((x[1] * y[0]) - (x[0] * y[1])) / (x[1] - x[0]);
            return new Tuple<double, double>(a, b);
        }

        public double GetDistance(Size modelObject)
        {
            var s = modelObject.Width > modelObject.Height ? modelObject.Width : modelObject.Height;
            var v =s==0?double.MaxValue:  (double) s* (480d / screenSize.Height);
            return (a*v)+b;
        }

        public double GetDiff(double distance)
        {
            return (a1 * distance) + b1;
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
