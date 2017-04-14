using AutoPilotApp.Common;
using Emgu.CV;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoPilotApp.Models
{
    public class Config : ObservableObject
    {
        public Config()
        {
            RedConfig = new ColorConfig();
            GreenConfig = new ColorConfig();
            SpaceConfig = new SpaceConfig();
        }

        private string droneIP;

        public string DroneIP
        {
            get { return droneIP; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "192.168.2.1";
                }
                Set(ref droneIP, value);
            }
        }


        private bool direction;
        [JsonIgnore]
        public bool Direction
        {
            get { return direction; }
            set { Set(ref direction, value); }
        }

        private ColorConfig redConfig;

        public ColorConfig RedConfig
        {
            get { return redConfig; }
            set
            {
                Set(ref redConfig, value);
            }
        }

        private ColorConfig greenConfig;

        public ColorConfig GreenConfig
        {
            get { return greenConfig; }
            set
            {
                Set(ref greenConfig, value);
            }
        }

        private SpaceConfig spaceConfig;

        public SpaceConfig SpaceConfig
        {
            get { return spaceConfig; }
            set { Set(ref spaceConfig , value); }
        }


        public void Save(string fileName)
        {
            using (StreamWriter w = new StreamWriter(fileName))
            {
                Save(w);
            }
        }

        public void Save(StreamWriter sw)
        {
            using (JsonTextWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, this, typeof(Config));
            }
        }

        public static Config Load(StreamReader reader)
        {
            using (JsonTextReader jw = new JsonTextReader(reader))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Error += (ser, err) =>
                {
                    Logger.LogError(err.ErrorContext.Error.Message);
                    err.ErrorContext.Handled = true;
                };
                return serializer.Deserialize<Config>(jw);
            }
        }
    }
}