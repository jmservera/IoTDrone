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
        int headCount;
        public int HeadCount
        {
            get { return headCount; }
            set { Set(ref headCount, value); }
        }
    }
}
