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
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

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
                if (callAPI) await getEmotion();

                var bmp = (Bitmap)input.Bitmap.Clone();
                if (faces != null)
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        lock (facesLock)
                        {
                            foreach (var face in faces)
                            {
                                var faceRec = face.FaceRectangle;
                                var emotions = face.FaceAttributes.Emotion.ToRankedList();
                                var emotion = emotions.OrderByDescending(f => f.Value).FirstOrDefault().Key;

                                g.DrawRectangle(new Pen(new SolidBrush(System.Drawing.Color.Yellow), 2), new System.Drawing.Rectangle(faceRec.Left, faceRec.Top, faceRec.Width, faceRec.Height + 45));
                                g.FillRectangle(new SolidBrush(System.Drawing.Color.Yellow), new System.Drawing.Rectangle(faceRec.Left, faceRec.Top + faceRec.Height, faceRec.Width, 45));
                                g.DrawString(emotion, new Font("Arial", 18), new SolidBrush(System.Drawing.Color.Black), faceRec.Left + 5, faceRec.Top + faceRec.Height + 2);
                            }
                        }
                        g.Flush();
                    }
                }
                output.UpdateImages(bmp);

            }
        }

        Face[] faces;
        object facesLock = new object();

        public async Task getEmotion()
        {
            callAPI = false;
            try
            {
                FaceServiceClient sc = new FaceServiceClient(ConfigurationManager.AppSettings["CognitiveKey"]);

                Byte[] byteArray = ImageToByte2(input.Bitmap);
                using (Stream imageFileStream = new MemoryStream(byteArray))
                {
                    var requiredFaceAttributes = new FaceAttributeType[] {
                        FaceAttributeType.Age,
                        FaceAttributeType.Gender,
                        FaceAttributeType.Smile,
                        FaceAttributeType.FacialHair,
                        FaceAttributeType.HeadPose,
                        FaceAttributeType.Glasses,
                        FaceAttributeType.Emotion
                    };
                    
                    var facesResult = await sc.DetectAsync(imageFileStream,
                        returnFaceLandmarks: true,
                        returnFaceAttributes: requiredFaceAttributes);
                    lock (facesLock)
                    {
                        faces = facesResult;
                    }
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

                            var gender = attributes.Gender;
                            output.Gender = gender;

                            var glasses = attributes.Glasses;
                            output.Glasses = glasses.ToString();

                            var smile = face.FaceAttributes.Smile;
                            output.Smiling = smile;
                            
                        }
                    }

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
