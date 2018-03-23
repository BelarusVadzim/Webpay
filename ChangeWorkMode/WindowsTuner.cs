using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeWorkMode
{
    class WindowsTuner
    {
        #region Public methods
        public int ActivateCustomerMode()
        {
            try
            {
                EnableTAskManager(false);
                EnableWinKey(false);
                ReplaceExplorerWithOurApp(false);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        public int ActivateNormalMode()
        {
            try
            {
                EnableTAskManager(true);
                EnableWinKey(true);
                ReplaceExplorerWithOurApp(true);
                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 2;
            }
        }
        #endregion

        #region Private methods
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
                System.AppDomain.CurrentDomain.BaseDirectory,  "WebPlace"));
            }
        }

        private void SetRegistryValue(RegistryHive registryHive, string SubKey,
            string ValueName, RegistryValueKind RegistryValueType, object Value)
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
        #endregion
    }
}
