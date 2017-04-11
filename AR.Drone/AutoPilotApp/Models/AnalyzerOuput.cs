using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Models
{
    public class AnalyzerOuput:ObservableObject
    {
        private Point point;

        public Point Center
        {
            get { return point; }
            set { Set(ref point , value); }
        }

        private double distance;

        public double Distance
        {
            get { return distance; }
            set { Set(ref distance , value); }
        }

    }
}
