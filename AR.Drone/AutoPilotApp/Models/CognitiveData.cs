using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutoPilotApp.Models
{
    public class CognitiveData : BitmapsBase
    {
        float headCount;
        double smiling;
        double age;
        String gender;
        String glasses;
        Rectangle square;
        
        public float HeadCount
        {
            get { return headCount; }
            set { Set(ref headCount, value); }
        }
        public Rectangle Square
        {
            get { return square; }
            set { Set(ref square, value); }
        }
        public double Smiling
        {
            get { return smiling; }
            set { Set(ref smiling, value); }
        }
        public double Age
        {
            get { return age; }
            set { Set(ref age, value); }
        }
        public String Gender
        {
            get { return gender; }
            set { Set(ref gender, value); }
        }
        public String Glasses
        {
            get { return glasses; }
            set { Set(ref glasses, value); }
        }


    }

    public class Rectangle
    {
        double width;
        double height;
        double left;
        double top;

        public Rectangle(double width_, double height_, double left_, double top_)
        {
            width = width_;
            this.height = height_;
            this.left = left_;
            this.top = top_;
        }
        public double Width
        {
            get { return width; }
        }
        public double Height
        {
            get { return height; }
        }
        public double Left
        {
            get { return left; }
        }
        public double Top
        {
            get { return top; }
        }
    }
}
