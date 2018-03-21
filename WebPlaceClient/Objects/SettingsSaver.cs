using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPlace.Objects
{
    class SettingsSaver
    {
        public void SaveSettings()
        {
            using (TextWriter tw = new StreamWriter(WebPlaceSettings.ConfigUri))
            {
                GeneralSettings GS = new Objects.GeneralSettings()
                {
                    //StartUrl = WebPaySettings.StartUrl,
                    PasswordHash = WebPlaceSettings.PasswordHash,
                    CustomerMode = WebPlaceSettings.CustomerMode
                };
                XmlSerializer xs = new XmlSerializer(typeof(GeneralSettings));
                xs.Serialize(tw, GS);
            }
        }
    }
    [Serializable]
    public class GeneralSettings
    {
        //public string StartUrl {get; set;}
        public string PasswordHash { get; set; }
        public bool CustomerMode { get; set; }
    }
}
