using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutoPilotApp.Models
{
    public class ColorConfig : ObservableObject
    {
        [JsonIgnore]
        public Color Low
        {
            get
            {
                return
                ColorUtils.HsvToRgb(this.lowH * 2, this.lowS / 255d, this.lowV / 255d);
            }
        }

        [JsonIgnore]
        public Color High
        {
            get
            {
                return ColorUtils.HsvToRgb(this.highH * 2, this.highS / 255d, this.highV / 255d);
            }
        }

        int lowH;

        public int LowH
        {
            get { return lowH; }
            set
            {
                Set(ref lowH, value);
                RaisePropertyChanged(new PropertyChangedEventArgs(nameof(Low)));
            }
        }

        private int highH;

        public int HighH
        {
            get { return highH; }
            set
            {
                Set(ref highH, value);
                RaisePropertyChanged(new PropertyChangedEventArgs(nameof(High)));
            }
        }

        private int lowS;

        public int LowS
        {
            get { return lowS; }
            set
            {
                Set(ref lowS, value);
                RaisePropertyChanged(new PropertyChangedEventArgs(nameof(Low)));
            }
        }

        private int highS;

        public int HighS
        {
            get { return highS; }
            set
            {
                Set(ref highS, value);
                RaisePropertyChanged(new PropertyChangedEventArgs(nameof(High)));
            }
        }

        private int lowV;

        public int LowV
        {
            get { return lowV; }
            set
            {
                Set(ref lowV, value);
                RaisePropertyChanged(new PropertyChangedEventArgs(nameof(Low)));
            }
        }

        private int highV;

        public int HighV
        {
            get { return highV; }
            set
            {
                Set(ref highV, value);
                RaisePropertyChanged(new PropertyChangedEventArgs(nameof(High)));
            }
        }
    }
}
