using AR.Drone.Infrastructure;
using AutoPilotApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace AutoPilotApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                    string ffmpegPath = string.Format(@"../../../AR.Drone/FFmpeg.AutoGen/FFmpeg/bin/windows/{0}", Environment.Is64BitProcess ? "x64" : "x86");
                    InteropHelper.RegisterLibrariesSearchPath(ffmpegPath);
                    break;
                case PlatformID.Unix:
                case PlatformID.MacOSX:
                    string libraryPath = Environment.GetEnvironmentVariable(InteropHelper.LD_LIBRARY_PATH);
                    InteropHelper.RegisterLibrariesSearchPath(libraryPath);
                    break;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var config = this.Resources["Config"] as Config;
            if (config != null)
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                using (StreamWriter sw = new StreamWriter(new IsolatedStorageFileStream("config.json", FileMode.Create, isolatedStorage)))
                {
                    config.Save(sw);
                }
            }

            base.OnExit(e);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var config = this.Resources["Config"] as Config;
            if (config != null)
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                if (isolatedStorage.FileExists("config.json"))
                {
                    using (StreamReader sr = new StreamReader(new IsolatedStorageFileStream("config.json", FileMode.Open, isolatedStorage)))
                    {
                        var newConfig = Config.Load(sr);
                        if (newConfig != null)
                        {
                            this.Resources["Config"] = newConfig;
                        }
                    }
                }
            }
            base.OnStartup(e);
        }
    }
}
