using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace PhotoBooth
{
    public static class Settings
    {
        private static Dictionary<string, string> SettingsCache = new Dictionary<string, string>();

        public static string ImagePath
        {
            get
            {
                if (SettingsCache.ContainsKey("ImagePath")) return SettingsCache["ImagePath"];

                string imagePath = ConfigurationManager.AppSettings["ImagePath"];
                if (String.IsNullOrEmpty(imagePath))
                {
                    imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EasyPhotoBooth");
                    SetSetting("ImagePath", imagePath);
                    if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
                }
                return imagePath;
            }
            set
            {
                SetSetting("ImagePath", value);
            }
        }

        public static string OverlaysPath
        {
            get
            {
                if (SettingsCache.ContainsKey("FramesPath")) return SettingsCache["FramesPath"];

                string imagePath = ConfigurationManager.AppSettings["FramesPath"];
                if (String.IsNullOrEmpty(imagePath))
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Overlays");
                    SetSetting("FramesPath", imagePath);
                    if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
                }
                return imagePath;
            }
            set
            {
                SetSetting("FramesPath", value);
            }
        }

        public static bool CycleOverlays
        {
            get
            {
                if (SettingsCache.ContainsKey("CycleFrames")) return Convert.ToInt32(SettingsCache["CycleFrames"]) == 1;

                int cycleFrames = int.TryParse(ConfigurationManager.AppSettings["CycleFrames"], out cycleFrames) ? cycleFrames : 0;
                return cycleFrames == 1;
            }
            set
            {
                SetSetting("CycleFrames", value ? "1" : "0");
            }
        }

        public static int SecondsToWait
        {
            get
            {
                if (SettingsCache.ContainsKey("SecondsToWait")) return Convert.ToInt32(SettingsCache["SecondsToWait"]);

                int secondsToWait = int.TryParse(ConfigurationManager.AppSettings["SecondsToWait"], out secondsToWait) ? secondsToWait : 0;
                if (secondsToWait <= 0) secondsToWait = 3;
                return secondsToWait;
            }
            set
            {
                SetSetting("SecondsToWait", value.ToString());
            }
        }

        public static int Rounds
        {
            get
            {
                if (SettingsCache.ContainsKey("Rounds")) return Convert.ToInt32(SettingsCache["Rounds"]);

                int rounds = int.TryParse(ConfigurationManager.AppSettings["Rounds"], out rounds) ? rounds : 0;
                if (rounds <= 0) rounds = 3;
                return rounds;
            }
            set
            {
                SetSetting("Rounds", value.ToString());
            }
        }

        public static int SecondsToPreview
        {
            get
            {
                if (SettingsCache.ContainsKey("SecondsToPreview")) return Convert.ToInt32(SettingsCache["SecondsToPreview"]);

                int secondsToPreview = int.TryParse(ConfigurationManager.AppSettings["SecondsToPreview"], out secondsToPreview) ? secondsToPreview : 0;
                if (secondsToPreview <= 0) secondsToPreview = 10;
                return secondsToPreview;
            }
            set
            {
                SetSetting("SecondsToPreview", value.ToString());
            }
        }

        private static void SetSetting(string key, string value)
        {
            if (SettingsCache.ContainsKey(key)) SettingsCache.Remove(key);
            SettingsCache.Add(key, value);

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            configuration.AppSettings.Settings.Remove(key);
            configuration.AppSettings.Settings.Add(key, value);
            configuration.Save(ConfigurationSaveMode.Minimal, true);
            ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
        }
    }
}
