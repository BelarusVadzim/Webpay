using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebPay.Objects;

namespace WebPay.Browser
{
    class BrowserConfigurator
    {
        public ChromiumWebBrowser ChromeBrowser { get; }

        private CefSettings Settings { get; set; }
        private LifespanHandler Life { get; set; }
        private KeyboardHandler1 keyboardHandler { get; set; }
        public event Action<Object, System.Windows.Forms.PreviewKeyDownEventArgs> BrowserKeyPressed;


        public BrowserConfigurator()
        {
            Settings = new CefSettings();
            Settings.CefCommandLineArgs.Add("enable-npapi", "1");
            Settings.CefCommandLineArgs.Add("ppapi-flash-path", @"pepflashplayer.dll");
            Settings.CefCommandLineArgs.Add("ppapi-flash-version", "20.0.0.306");
            
            Cef.Initialize(Settings);
            Life = new LifespanHandler();
            keyboardHandler = new KeyboardHandler1();
            ChromeBrowser = new ChromiumWebBrowser(WebPaySettings.StartUrl);
            Life.PopupRequest += LifePopupRequest;
            ChromeBrowser.LifeSpanHandler = Life;
            ChromeBrowser.PreviewKeyDown += ChromeBrowser_PreviewKeyDown;
            ChromeBrowser.AddressChanged += ChromeBrowserAddressChanged;
            ChromeBrowser.KeyboardHandler = keyboardHandler;
            HideScrolBars(true);
            

        }

        private void ChromeBrowser_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F1)
            {
                BrowserKeyPressed?.Invoke(sender, e);
            }

        }



        private void ChromeBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            string s = e.Address.ToLower();
            foreach (var item in WebPaySettings.WhiteList)
            {
                if (s.Contains(item))
                    return;
            }
            e.Browser.StopLoad();
            e.Browser.GoBack();
        }

        private void LifePopupRequest(string obj)
        {
            Life.EnablePopup = false;
            foreach (var WhiteItem in WebPaySettings.WhiteList)
            {
                if (obj.Contains(WhiteItem))
                {
                    Life.EnablePopup = true;
                    break;
                }
            }
            foreach (var BlackItem in WebPaySettings.BlackList)
            {
                if (obj.Contains(BlackItem))
                {
                    Life.EnablePopup = false;
                    return;
                }
            }

        }

        private void HideScrolBars(Boolean hide)
        {
            if(hide)
               ChromeBrowser.FrameLoadEnd += OnBrowserFrameLoadEnd;
        }

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
        {
            if (args.Frame.IsMain)
            {
                args
                    .Browser
                    .MainFrame
                    .ExecuteJavaScriptAsync(
                    "document.body.style.overflow = 'hidden'");
            }
        }

        public string GetTextContentByElementId(string ElementId)
        {
            string startDate = "";
            string script = string.Format("document.getElementById('{0}').textContent;", ElementId);
            ChromeBrowser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;

                if (response.Success && response.Result != null)
                {
                    startDate = response.Result.ToString();
                }
            });
            return startDate;
        }
    }

}
