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
        Boolean callAPI = true;
        Boolean callAPI2 = false;
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

        private async void Input_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Bitmap")
            {

                float result = 1000;
                var bmp = (Bitmap) input.Bitmap.Clone();
                //todo call api                
                output.UpdateImages(bmp);
                if(callAPI) result = await getEmotion(bmp);
            }
        }

        public async Task<float> getEmotion(Bitmap bits)
        {
            callAPI = false;
            float emotion = 150;
            try
            {

                EmotionServiceClient emotionServiceClient = new EmotionServiceClient(ConfigurationManager.AppSettings["CognitiveKey"]);
                Emotion[] emotionResult;

                Byte[] byteArray = ImageToByte2(input.Bitmap);
                using (Stream imageFileStream = new MemoryStream(byteArray))
                {
                    Logger.LogInfo($"Llamo a la API");
                    emotionResult = await emotionServiceClient.RecognizeAsync(imageFileStream);
                    emotion = emotionResult[0].Scores.Happiness;
                    Logger.LogInfo($"Successfull call");
                    callAPI = true;
                    
                }


            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            output.HeadCount = emotion;
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
