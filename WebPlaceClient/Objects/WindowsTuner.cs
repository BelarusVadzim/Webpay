using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Objects
{
    class WindowsTuner
    {
        public void ActivateCustomerMode()
        {
            //EnableTAskManager(false);
            //EnableWinKey(false);
            ReplaceExplorerWithOurApp(false);
        }

        public void ActivateNormalMode()
        {
            EnableTAskManager(true);
            EnableWinKey(true);
            ReplaceExplorerWithOurApp(true);
        }


        private void EnableTAskManager(bool enableTaskManager)
        {
            int keyValue = enableTaskManager ? 0 : 1;

            SetRegistryValue(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Policies\System",
               "DisableTaskMgr", RegistryValueKind.DWord, keyValue);
        }

        private void EnableWinKey(bool enableWinKey)
        {
            int keyValue = enableWinKey ? 0 : 1;
            SetRegistryValue(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", 
                "NoWinKeys", RegistryValueKind.DWord, keyValue);
        }

        private void ReplaceExplorerWithOurApp(bool enableExplorer)
        {
            if (enableExplorer)
            {
                SetRegistryValue(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Policies\System",
                "Shell", RegistryValueKind.String, "Explorer.exe");
            }
            else
            {
                SetRegistryValue(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Policies\System",
                "Shell", RegistryValueKind.String, string.Format("{0}\\{1}.exe",
                Directory.GetCurrentDirectory(),  System.Reflection.Assembly.GetExecutingAssembly().GetName().Name));
            }
        }

        private void SetRegistryValue(RegistryHive registryHive, string SubKey, string ValueName, RegistryValueKind RegistryValueType, object Value)
        {

            using (RegistryKey myKey = Registry.CurrentUser.OpenSubKey(SubKey, true))
            {
                if(myKey == null)
                {
                    RegistryKey newKey = RegistryKey.OpenBaseKey(registryHive, RegistryView.Default);
                    newKey = newKey.CreateSubKey(SubKey);
                    newKey.SetValue(ValueName, Value, RegistryValueType);
                    newKey.Close();
                    return;
                }
                myKey.SetValue(ValueName, Value, RegistryValueType);
                myKey.Close();
            }
        }
    }
}
