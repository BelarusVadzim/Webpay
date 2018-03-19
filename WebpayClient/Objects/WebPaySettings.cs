using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebPay.Objects
{
    [Serializable]
    public static class WebPaySettings
    {
        

        private static string startUrl = "";

        public static List<string> BlackList { get; set; }

        public static string PasswordHash { get; set; }

        public static string StartUrl
        {
            get
            {
                  return   string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, startUrl);
            }
            set
            {
                startUrl = value;
            }
 
         }

        public static List<string> WhiteList { get; set; }

        public static void Load()
        {
            SettingsLoader Loader = new SettingsLoader();
            Loader.LoadSettings();
        }

        public static Dictionary<string, string> Params { get; set; }

        public static void Save()
        {
            SettingsSaver SS = new SettingsSaver();
            SS.SaveSettings(Params);
        }

    }
}
