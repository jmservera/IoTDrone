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
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Face;

namespace AutoPilotApp
{
    public class CognitiveController
    {
        Bitmaps input;
        CognitiveData output;
        Boolean callAPI = true;
        private static readonly VisualFeature[] VisualFeatures = { VisualFeature.Description, VisualFeature.Faces };
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
                //todo call api                
                if(callAPI)await getEmotion();
            }
        }

        public async Task getEmotion()
        {
            callAPI = false;
            try
            {
                FaceServiceClient sc = new FaceServiceClient(ConfigurationManager.AppSettings["CognitiveKey"]);

                Byte[] byteArray = ImageToByte2(input.Bitmap);
                using (Stream imageFileStream = new MemoryStream(byteArray))
                {
                    Logger.LogInfo($"Llamo a la API");
                    var requiredFaceAttributes = new FaceAttributeType[] {
                        FaceAttributeType.Age,
                        FaceAttributeType.Gender,
                        FaceAttributeType.Smile,
                        FaceAttributeType.FacialHair,
                        FaceAttributeType.HeadPose,
                        FaceAttributeType.Glasses,
                        FaceAttributeType.Emotion
                    };
                    var faces = await sc.DetectAsync(imageFileStream,
                        returnFaceLandmarks: true,
                        returnFaceAttributes: requiredFaceAttributes);
                    output.HeadCount = faces.Length;
                    output.Age = 0;
                    if (faces.Length > 0)
                    {
                        foreach (var face in faces)
                        {
                            var faceRec = face.FaceRectangle;
                            var attributes = face.FaceAttributes;
                            var rec = new Models.Rectangle(face.FaceRectangle.Width * 0.6, faceRec.Height * 0.6, faceRec.Left, faceRec.Top);
                            output.Square = rec;

                            var age = attributes.Age;
                            output.Age = output.Age>0? (output.Age + age)/2:age;
                            Logger.LogInfo($"Age: " + age);

                            var gender = attributes.Gender;
                            output.Gender = gender;
                            Logger.LogInfo($"Gender: " + gender);

                            var glasses = attributes.Glasses;
                            output.Glasses = glasses.ToString();
                            Logger.LogInfo($"Glases? " + glasses);

                            var smile = face.FaceAttributes.Smile;
                            output.Smiling = smile;
                            Logger.LogInfo($"Smiling? " + smile);
                            var emotions = face.FaceAttributes.Emotion.ToRankedList();
                            var emotion = emotions.OrderByDescending(f => f.Value).FirstOrDefault().Key;

                            using (Graphics g = Graphics.FromImage(input.Bitmap))
                            {
                                g.DrawRectangle(new Pen(new SolidBrush(System.Drawing.Color.Yellow),2),  new System.Drawing.Rectangle(faceRec.Left, faceRec.Top, faceRec.Width,faceRec.Height+45));
                                g.FillRectangle(new SolidBrush(System.Drawing.Color.Yellow), new System.Drawing.Rectangle(faceRec.Left, faceRec.Top + faceRec.Height , faceRec.Width, 45));
                                g.DrawString(emotion, new Font("Arial", 18), new SolidBrush(System.Drawing.Color.Black), faceRec.Left + 5, faceRec.Top + faceRec.Height +2);
                                g.Flush();
                            }
                            output.UpdateImages((Bitmap)input.Bitmap);
                        }
                    }
                    Logger.LogInfo($"Successfull call");


                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            finally
            {
                callAPI = true;
            }
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
