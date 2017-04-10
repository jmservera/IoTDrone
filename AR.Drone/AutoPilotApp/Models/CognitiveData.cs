using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AutoPilotApp.Models
{
    public class CognitiveData: BitmapsBase
    {
        public BitmapSource Image
        {
            get { return wbitmaps[0]; }
        }
    }
}
