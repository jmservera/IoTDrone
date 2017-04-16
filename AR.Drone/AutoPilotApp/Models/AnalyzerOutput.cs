using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp.Models
{
    public class AnalyzerOutput : ObservableObject
    {
        bool detected;
        public bool Detected
        {
            get { return detected; }
            set { Set(ref detected, value); }
        }
        private Point point;

        public Point Center
        {
            get { return point; }
            set { Set(ref point, value); }
        }

        private double distance;

        public double Distance
        {
            get { return distance; }
            set { Set(ref distance, value); }
        }

        Size size;
        public Size FovSize
        {
            get { return size; }
            set { Set(ref size, value); }
        }

        string command;
        public string ResultingCommand
        {
            get { return command; }
            set { Set(ref command, value); }
        }

        int width, height;
        public int Width
        {
            get { return width; }
            set
            {
                Set(ref width, value);
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                Set(ref height, value);
            }
        }

        public Navigation Navigation { get; private set; } = new Navigation();

        bool start;
        public bool Start { get { return start; } set { Set(ref start, value); } }
    }
}
