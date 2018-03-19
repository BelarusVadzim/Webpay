using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using WebPay.Objects;
using WebPay.Forms;
using System.Diagnostics;
using System.Security.Permissions;
using System.Security.Principal;

namespace WebPay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeChromium();
        }

        private Boolean canClose = false;
        Browser.BrowserConfigurator Bro;
        private void InitializeChromium()
        {
          try
            {
                Bro = new Browser.BrowserConfigurator();

                //main browser

                this.panel1.Controls.Add(Bro.ChromeBrowser);
                Bro.ChromeBrowser.Dock = DockStyle.Fill;
                Bro.BrowserKeyPressed += OnBrowserKeyPressed;

            }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in initializing the browser. Error: " + ex.Message);
                }
        }

        private void InitializeSettings()
        {
            WebPaySettings.Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canClose = true;
            ApplyModeChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkModeChanger.SetupNormalMode();
            DebugInfoMethod("ActivateNormalMode");
            ApplyModeChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkModeChanger.SetupCustomerMode();
            DebugInfoMethod("ActivateCustomerMode");
            ApplyModeChanges();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canClose)
            {
                e.Cancel = false;
                Cef.Shutdown();
                return;
            }
            e.Cancel = true;

        }

        private void OnBrowserKeyPressed(object sender, PreviewKeyDownEventArgs e)
        {
            
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F1)
            {
                ShowPasswordWindow();
            }
        }

        private void ShowPasswordWindow()
        {
            using (AuthenticationForm passwordForm = new Forms.AuthenticationForm())
            {
                if (DialogResult.OK == passwordForm.ShowDialog())
                {
                    ShowControlPanel();
                }
                passwordForm.Close();
            }
        }


        private void ShowControlPanel()
        {
            panel2.Visible = true;
            panel1.Top = 50;
        }

        private void HideControlPanel()
        {
            panel2.Visible = false;
            panel1.Top = 0;
        }

        private void ApplyModeChanges()
        {
#if DEBUG
            DebugInfoMethod("Debug mode. Exit without reboot.");
#else
            //System.Diagnostics.Process.Start("ShutDown", "/r /t 0 /f");
            MessageBox.Show("Autoreboot is off");
            //LogOffUser();
#endif
            this.Close();
        }

        
        public static void DebugInfoMethod(string message)
        {
#if DEBUG
            MessageBox.Show(message);
#endif
        }

        [DllImport("user32.dll")]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        private void LogOffUser()
        {
            ExitWindowsEx(0, 0);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F1)
            {
                ShowPasswordWindow();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebPaySettings.Save();
        }
    }
}
