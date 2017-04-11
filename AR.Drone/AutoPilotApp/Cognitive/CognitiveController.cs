using AutoPilotApp.Common;
using AutoPilotApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPilotApp
{
    public class CognitiveController
    {
        Bitmaps input;
        CognitiveData output;
        public CognitiveController(Bitmaps input, CognitiveData output)
        {
            this.input = input;
            this.output = output;

            try
            {
                var cogKey = ConfigurationManager.AppSettings["CognitiveKey"];
                Logger.LogInfo($"Cognitive Key: {cogKey}");
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
            }

            input.PropertyChanged += Input_PropertyChanged;
        }

        private void Input_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Bitmap")
            {
                var bmp = (Bitmap) input.Bitmap.Clone();
                //todo call api
                output.HeadCount = 10;
                output.UpdateImages(bmp);
            }
        }
    }
}
