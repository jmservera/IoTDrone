using AutoPilotApp.Common;
using AutoPilotApp.Models;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
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
                String result = getEmotion(input).Result;
                output.HeadCount = 10;
                output.UpdateImages(bmp);
            }
        }

        public async Task<String> getEmotion(Bitmaps bits)
        {
            String emotion = "";
            try
            {

                EmotionServiceClient emotionServiceClient = new EmotionServiceClient(ConfigurationManager.AppSettings["CognitiveKey"]);
                Emotion[] emotionResult;

                Byte[] byteArray = ImageToByte2(input.Bitmap);
                using (Stream imageFileStream = new MemoryStream(byteArray))
                {
                    emotionResult = await emotionServiceClient.RecognizeAsync(imageFileStream);
                }


            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }

            return emotion;
        }

        public static byte[] ImageToByte2(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
