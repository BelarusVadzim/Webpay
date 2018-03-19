using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.WinForms;

namespace WebPay.Browser
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
            return !EnablePopup;
        }
        bool ILifeSpanHandler.DoClose(IWebBrowser browserControl, IBrowser browser)
        { return false; }

        void ILifeSpanHandler.OnBeforeClose(IWebBrowser browserControl, IBrowser browser) { }

        void ILifeSpanHandler.OnAfterCreated(IWebBrowser browserControl, IBrowser browser) { }
    }
}
