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
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using AutoPilotApp.Cognitive;
using System.Diagnostics;

namespace AutoPilotApp
{
    public class CognitiveController
    {
        Bitmaps input;
        CognitiveData output;
        Boolean callAPI = true;
        IEnumerable<IdentifiedPerson> IdentifiedPersons { get; set; }


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
            catch (Exception ex)
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
                using (FaceServiceClient sc = new FaceServiceClient(ConfigurationManager.AppSettings["CognitiveKey"])) {
                    //await checkKnownFace(sc);
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
                        Debug.WriteLine("Entro a AddFaces");
                        await AddFaces(sc);
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
                                output.Age = output.Age > 0 ? (output.Age + age) / 2 : age;

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

        public async Task<String> checkKnownFace(FaceServiceClient sc)
        {
            Debug.WriteLine("Entro en checkKnownFaces");
            try
            {
                Person[] persons = await sc.GetPersonsAsync("Dani");
                Debug.WriteLine("Numero de personas: " + persons.Count());
                Debug.WriteLine("Nombre personas: " + persons[0].Name);
                return persons[0].Name;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return "Nada";
        }

        public async Task AddFaces(FaceServiceClient sc)
        {
            String workspace = "3d8633fe-7b6d-4faa-85e1-fd9d09e73f51";
            PersonGroup pg = await sc.GetPersonGroupAsync(workspace);
            Debug.WriteLine("PersonGroup: " + pg.Name);


            Guid guid = new Guid("cfadcd8a-0bb2-4c93-a03a-264763ca748f");
            Person per = await sc.GetPersonAsync("3dd9f493-8e7e-4703-86a7-0eb0b628ebfd", guid);
            Debug.WriteLine("Termino de entrenar: " + per.Name);

            /*await sc.CreateFaceListAsync("face", "face");
            Guid personGuid = new Guid();
            await sc.AddPersonFaceAsync("personGroupId", personGuid, "C:/Users/t-daorti/Pictures/Camera Roll/WIN_20170412_11_16_54_Pro");
            await sc.TrainPersonGroupAsync("personGroupId");
            Person[] per = await sc.GetPersonsAsync("personGroupId");*/

        }


    }
}
