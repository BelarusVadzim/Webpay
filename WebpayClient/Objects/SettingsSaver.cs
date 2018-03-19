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
        public void SaveSettings(Dictionary<string, string> dictionaryOfSettings)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Dictionary<string, string>));
            TextWriter tw = new StreamWriter(CurentAppDirectory.CreateFullPathForFile("WebPay.cfg"));
            xs.Serialize(tw, dictionaryOfSettings);
        }
    }
}
