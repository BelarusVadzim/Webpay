using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Objects
{
    class SettingsSaver
    {
        public void SaveSettings()
        {
            using (TextWriter tw = new StreamWriter(WebPaySettings.ConfigUri))
            {
                GeneralSettings GS = new Objects.GeneralSettings()
                {
                    //StartUrl = WebPaySettings.StartUrl,
                    PasswordHash = WebPaySettings.PasswordHash,
                    CustomerMode = WebPaySettings.CustomerMode
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
