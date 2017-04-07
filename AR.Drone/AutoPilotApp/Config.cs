using Emgu.CV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoPilotApp
{
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>Occurs when a property value changes. </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Updates the property and raises the changed event, but only if the new value does not equal the old value. </summary>
        /// <param name="propertyName">The property name as lambda. </param>
        /// <param name="oldValue">A reference to the backing field of the property. </param>
        /// <param name="newValue">The new value. </param>
        /// <returns>True if the property has changed. </returns>
        public bool Set<T>(ref T oldValue, T newValue, [CallerMemberName] String propertyName = null)
        {
            return Set(propertyName, ref oldValue, newValue);
        }

        /// <summary>Updates the property and raises the changed event, but only if the new value does not equal the old value. </summary>
        /// <param name="propertyName">The property name as lambda. </param>
        /// <param name="oldValue">A reference to the backing field of the property. </param>
        /// <param name="newValue">The new value. </param>
        /// <returns>True if the property has changed. </returns>
        public virtual bool Set<T>(String propertyName, ref T oldValue, T newValue)
        {
            if (Equals(oldValue, newValue))
                return false;

            oldValue = newValue;
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
            return true;
        }

        /// <summary>Raises the property changed event. </summary>
        /// <param name="propertyName">The property name. </param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Raises the property changed event. </summary>
        /// <param name="args">The arguments. </param>
        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this,args);
        }

        /// <summary>Raises the property changed event for all properties (string.Empty). </summary>
        public void RaiseAllPropertiesChanged()
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(string.Empty));
        }
    }
    public class ColorConfig : ObservableObject
    {
        [JsonIgnore]
        public Color Low
        {
            get
            {
                return
                ColorUtils.HsvToRgb(this.lowH * 2, this.lowS  / 255d, this.lowV  / 255d);
            }
        }

        [JsonIgnore]
        public Color High
        {
            get
            {
               return ColorUtils.HsvToRgb(this.highH * 2, this.highS / 255d, this.highV  / 255d);
            }
        }

        int lowH;

        public int LowH
        {
            get { return lowH; }
            set { Set(ref lowH, value);
            RaisePropertyChanged(new PropertyChangedEventArgs(nameof(Low)));
            }
        }

        private int highH;

        public int HighH
        {
            get { return highH; }
            set { Set(ref highH , value);
            RaisePropertyChanged(new PropertyChangedEventArgs(nameof(High)));
            }
        }

        private int lowS;

        public int LowS
        {
            get { return lowS; }
            set { Set(ref lowS , value);
            RaisePropertyChanged(new PropertyChangedEventArgs(nameof(Low)));
            }
        }

        private int highS;

        public int HighS
        {
            get { return highS; }
            set { Set(ref highS , value);
            RaisePropertyChanged(new PropertyChangedEventArgs(nameof(High)));
            }
        }

        private int lowV;

        public int LowV
        {
            get { return lowV; }
            set { Set(ref lowV , value);
            RaisePropertyChanged(new PropertyChangedEventArgs(nameof(Low)));
            }
        }

        private int highV;

        public int HighV
        {
            get { return highV; }
            set { Set(ref highV , value);
            RaisePropertyChanged(new PropertyChangedEventArgs(nameof(High)));
            }
        }
    }

    public class Config:ObservableObject
    {
        public Config()
        {
            RedConfig = new ColorConfig();
            GreenConfig = new ColorConfig();
        }
        private bool direction;
        [JsonIgnore]
        public bool Direction
        {
            get { return direction; }
            set { Set(ref direction , value); }
        }

        private ColorConfig redConfig;

        public ColorConfig RedConfig
        {
            get { return redConfig; }
            set {
                Set(ref redConfig , value);
            }
        }

        private ColorConfig greenConfig;

        public ColorConfig GreenConfig
        {
            get { return greenConfig; }
            set { Set(ref greenConfig , value);
            }
        }
    }

    public class Bitmaps: ObservableObject
    {
        private long calculations;

        public long Calculations
        {
            get { return calculations; }
            set { Set(ref calculations , value); }
        }

        private long imageSet;

        public long ImageSet
        {
            get { return imageSet; }
            set { Set(ref imageSet , value); }
        }

        private long[] fps=new long[5];
        long index;
        public long FPS
        {
            get {
                long count = index < fps.Length ? index : fps.Length;
                return count>0? 1000/(fps.Take((int)count).Sum() / count):0;
            }
            set
            {
                fps[index++ % 5] = value;
                RaisePropertyChanged(nameof(FPS));
            }
        }


        private BitmapImage original;

        public BitmapImage Original
        {
            get { return original; }
            set { Set(ref original , value); }
        }

        private BitmapImage first;

        public BitmapImage First
        {
            get { return first; }
            set { Set(ref first , value); }
        }

        private BitmapImage second;

        public BitmapImage Second
        {
            get { return second; }
            set { Set(ref second , value); }
        }

        private BitmapImage final;

        public BitmapImage Final
        {
            get { return final; }
            set { Set(ref final , value); }
        }

        public System.Drawing.Bitmap Bitmap { get; set; }
    }

    public class ColorUtils
    {
        public static Color HsvToRgb(double h, double s, double v)
        {
            int hi = (int)Math.Floor(h / 60.0) % 6;
            double f = (h / 60.0) - Math.Floor(h / 60.0);

            double p = v * (1.0 - s);
            double q = v * (1.0 - (f * s));
            double t = v * (1.0 - ((1.0 - f) * s));

            Color ret;

            switch (hi)
            {
                case 0:
                    ret = ColorUtils.GetRgb(v, t, p);
                    break;
                case 1:
                    ret = ColorUtils.GetRgb(q, v, p);
                    break;
                case 2:
                    ret = ColorUtils.GetRgb(p, v, t);
                    break;
                case 3:
                    ret = ColorUtils.GetRgb(p, q, v);
                    break;
                case 4:
                    ret = ColorUtils.GetRgb(t, p, v);
                    break;
                case 5:
                    ret = ColorUtils.GetRgb(v, p, q);
                    break;
                default:
                    ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    break;
            }
            return ret;
        }
        public static Color GetRgb(double r, double g, double b)
        {
            return Color.FromArgb(255, (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
    }
}
