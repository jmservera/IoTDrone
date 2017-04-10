using System.Linq;
using System.Windows.Media.Imaging;

namespace AutoPilotApp.Models
{
    public class Bitmaps : BitmapsBase
    {
        private long calculations;

        public Bitmaps() : base(4)
        {

        }

        public long Calculations
        {
            get { return calculations; }
            set { Set(ref calculations, value); }
        }

        private long imageSet;

        public long ImageSet
        {
            get { return imageSet; }
            set { Set(ref imageSet, value); }
        }

        private long[] fps = new long[5];
        long index;
        public long FPS
        {
            get
            {
                long count = index < fps.Length ? index : fps.Length;
                return count > 0 ? 1000 / (fps.Take((int)count).Sum() / count) : 0;
            }
            set
            {
                fps[index++ % 5] = value;
                RaisePropertyChanged(nameof(FPS));
            }
        }

        public BitmapSource First
        {
            get { return wbitmaps[1]; }
        }

        public BitmapSource Second
        {
            get { return wbitmaps[2]; }
        }

        public BitmapSource Final
        {
            get { return wbitmaps[3]; }
        }

    }
}
