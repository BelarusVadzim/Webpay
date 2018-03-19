﻿using CsQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebPay.Objects
{
    class SettingsLoader
    {
        public void LoadSettings()
        {
            DummyLoad();
            LoadFromFile();
            LoadBlakWhiteListFromUrl(WebPaySettings.StartUrl);
        }

        private void DummyLoad()
        {
            //WebPaySettings.WhiteList = new List<string>
            //{
            //    "oncebet.com",
            //    "mismarcadores.com",
            //    "coffeebeansgames",
            //    "indexwebpay",
            //    "tut.by"
            //};

            //WebPaySettings.BlackList = new List<string>
            //{
            //    @"mismarcadores.com/promobox/4068/"
            //};

            //WebPaySettings.StartUrl = "indexwebpay.html";
            //WebPaySettings.PasswordHash = "softacomhash";
        }

        private void  LoadFromFile()
        {

            using (FileStream fs = new FileStream(WebPaySettings.ConfigUri, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(GeneralSettings));

                try
                {
                    GeneralSettings GS = (GeneralSettings)formatter.Deserialize(fs);
                    WebPaySettings.PasswordHash = GS.PasswordHash;
                    WebPaySettings.StartUrl = GS.StartUrl;
                    WebPaySettings.CustomerMode = GS.CustomerMode;
                }
                catch
                {
                    WebPaySettings.StartUrl = CurentAppDirectory.CreateFullPathForFile("indexwebpay.html");
                    WebPaySettings.PasswordHash = "";
                    WebPaySettings.FirstBoot = true;
                }
            }
        }

        private void LoadBlakWhiteListFromUrl(string Url)
        {
            string html = GetHtmlFromUrl(WebPaySettings.StartUrl);
            LoadBlacklistFromHtml(html);
            LoadWhitelistFromHtml(html);
        }

        private void LoadBlacklistFromHtml(string html)
        {
            LoadlistFromHtml(html, "Blacklist");
        }

        private void LoadWhitelistFromHtml(string html)
        {
            LoadlistFromHtml(html, "Whitelist");
        }

        private void LoadlistFromHtml(string html, string propName)
        {
            string settingsString = "";
            IEnumerable<string> blacklist;
            settingsString = GetInnerTextByElementId(html, propName);
            blacklist = ParseSettingsFromString(settingsString);
            List<string> L = new List<string>();
            foreach (var item in blacklist)
            {
                L.Add(item);
            }
            switch (propName)
            {
                case "Blacklist":
                    WebPaySettings.BlackList=L;
                    break;
                case "Whitelist":
                    WebPaySettings.WhiteList=L;
                    break;
                default:
                    break;
            }
            
            
        }

        private IEnumerable<string> ParseSettingsFromString(string settingsString)
        {
            string s = "";
            List<string> ss;
            List<string> result = new List<string>();
            s = settingsString.Trim();
            ss = s.Split(new char[] { ';', ',', ' ' }).ToList<string>();
            foreach (var item in ss)
            {
                result.Add(item.Trim());
            }
            return result;
        }

        private string GetHtmlFromUrl(string Url)
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(Url);
                return htmlCode;
            }
        }

        private string GetInnerTextByElementId(string html, string elementId)
        {
            string result = "";
            CQ cq = CQ.Create(html);
            foreach (IDomObject obj in cq.Find(string.Format("#{0}", elementId)))
                result = obj.InnerText;
            return result;
        }
    }
}
