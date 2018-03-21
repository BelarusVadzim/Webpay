using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.WinForms;

namespace WebPlace.Browser
{
    public class LifespanHandler : ILifeSpanHandler
    {
        //event that receive url popup
        public event Action<string> PopupRequest;
        public bool EnablePopup { get; set; }

        bool ILifeSpanHandler.OnBeforePopup(IWebBrowser browserControl, 
            IBrowser browser, IFrame frame, string targetUrl, 
            string targetFrameName, WindowOpenDisposition targetDisposition, 
            bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, 
            IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            
            //get url popup
            PopupRequest?.Invoke(targetUrl);
            
            //stop open popup window
            newBrowser = null;
            //return !EnablePopup;
            if (EnablePopup)
                //browserControl.ExecuteScriptAsync("alert('test');");
                OpenPopupInIframe(browserControl, targetUrl);
            return true;
        }
        bool ILifeSpanHandler.DoClose(IWebBrowser browserControl, IBrowser browser)
        { return false; }

        void ILifeSpanHandler.OnBeforeClose(IWebBrowser browserControl, IBrowser browser) { }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser browserControl, IBrowser browser) { }


        private void OpenPopupInIframe(IWebBrowser browserControl, string Url)
        {
            StringBuilder script = new StringBuilder();
            //script.Append(string.Format("alert('{0}');", Url));
            //script.Append(" $(\"#ButtonGame\").show();");
            //script.Append("$(\"#ButtonGame\").css(\"visibility\", \"visible\");");
            script.Append(string.Format("$(\"#SiteGame\").prop(\"src\", \"{0}\");", Url));
            script.Append("$(\"iframe\").hide();");
            script.Append("$(\"#GameDiv\").show();");
            script.Append("$(\"#GameDiv\").css(\"visibility\", \"visible\");");
            script.Append("$(\"#SiteGame\").show();");
            script.Append("$(\"#SiteGame\").css(\"visibility\", \"visible\");");
            script.Append("$(\"#mySidenav\").hide();");
            browserControl.ExecuteScriptAsync(script.ToString());
        }
    }
}
