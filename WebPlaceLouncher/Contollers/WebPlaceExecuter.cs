using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlaceLouncher.Contollers
{
    class WebPlaceExecuter
    {
        public void RunWebPLace(string WebPlaceExePath)
        {
            Process ExternalProcess = new Process();
            //ExternalProcess.StartInfo.FileName = "Notepad.exe";
            ExternalProcess.StartInfo.FileName = WebPlaceExePath;
            ExternalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            ExternalProcess.Start();
        }
    }
}
