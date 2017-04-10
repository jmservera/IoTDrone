using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoPilotApp.Models
{
    public class BitmapsBase : ObservableObject
    {
        public BitmapSource Original
        {
            get { return wbitmaps[0]; }
        }

        public BitmapsBase(int bitmaps = 1)
        {
            wbitmaps = new WriteableBitmap[bitmaps];
        }

        public System.Drawing.Bitmap Bitmap { get; private set; }

        protected WriteableBitmap[] wbitmaps;
        int init;
        public void UpdateImages(params System.Drawing.Bitmap[] bitmaps)
        {
            if (bitmaps[0] != null)
            {
                Bitmap = (System.Drawing.Bitmap)bitmaps[0].Clone();
            }
            if (init < ((1 << wbitmaps.Length) - 1))
            {
                for (int i = 0; i < bitmaps.Length; i++)
                {
                    if (bitmaps[i] != null)
                    {
                        init = init | (1 << i);
                        var bmp = bitmaps[i];
                        wbitmaps[i] = new WriteableBitmap(
                            bmp.Width, bmp.Height, bmp.HorizontalResolution, bmp.VerticalResolution, ConvertPixelFormat(bmp.PixelFormat), null);
                    }
                }
            }
            for (int i = 0; i < bitmaps.Length; i++)
            {
                if (bitmaps[i] != null)
                {
                    DrawImage(wbitmaps[i], bitmaps[i], bitmaps[i].PixelFormat);
                }
            }

            RaiseAllPropertiesChanged();
        }

        static PixelFormat ConvertPixelFormat(System.Drawing.Imaging.PixelFormat sourceFormat)
        {
            switch (sourceFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    return PixelFormats.Bgr24;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    return PixelFormats.Bgra32;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    return PixelFormats.Bgr32;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    return PixelFormats.Gray8;

                    // .. as many as you need...
            }
            return new System.Windows.Media.PixelFormat();
        }

        public void DrawImage(WriteableBitmap writeableBitmap, System.Drawing.Bitmap bitmap, System.Drawing.Imaging.PixelFormat format)
        {
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, format);
            try
            {
                writeableBitmap.Lock();
                writeableBitmap.WritePixels(new Int32Rect(0, 0, bitmap.Width, bitmap.Height),
                    data.Scan0,
                    data.Height * data.Stride, data.Stride, 0, 0);
                writeableBitmap.Unlock();
            }
            finally
            {
                bitmap.UnlockBits(data);
                bitmap.Dispose();
            }
        }
    }
}
