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
using WebPlace.Objects;
using WebPlace.Forms;
using System.Diagnostics;
using System.Security.Permissions;
using System.Security.Principal;
using WebPlace.Browser;

namespace WebPlace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeChromium();
            InitializeUIElements();
        }

        private Boolean canClose = false;
        private BrowserConfigurator Bro;

        #region Initialize methods
        private void InitializeChromium()
        {
          try
            {
                Bro = new BrowserConfigurator();

                //main browser

                this.panel1.Controls.Add(Bro.ChromeBrowser);
                Bro.ChromeBrowser.Dock = DockStyle.Fill;
                Bro.BrowserKeyPressed += OnBrowserKeyPressed;
            }
                catch (Exception ex)
                {
                    DebugMessageBox("Error in initializing the browser. Error: " + ex.Message);
                }
        }

        private void InitializeSettings()
        {
            try
            {
                WebPlaceSettings.Load();
            }
            catch(Exception ex)
            {
                DebugMessageBox(ex.Message);
            }
            
        }

        private void InitializeUIElements()
        {
            if (WebPlaceSettings.PasswordHash == "")
            {
                ShowLoginWindow();
            }
                
            
            if (WebPlaceSettings.CustomerMode)
            {
                linkLabelStartCustomer.Enabled = false;
                linkLabelStartNormal.Enabled = true;
                label1.Text = "Customer mode";
                label1.ForeColor = Color.Green;
            }
            else
            {
                linkLabelStartCustomer.Enabled = true;
                linkLabelStartNormal.Enabled = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.canClose = true;
                this.WindowState = FormWindowState.Normal;
                ShowControlPanel();
                linkLabel4.Enabled = false;
                linkLabel2.Enabled = false;
            }


        }
        #endregion

        #region Event handlers
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canClose)
            {
                WebPlaceSettings.Save();
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
                OpenControlPanel();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F1)
            {
                OpenControlPanel();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (ChangePasswordForm changePasswordForm = new ChangePasswordForm())
            {
                changePasswordForm.ShowDialog();
            }
        }

        private void linkLabelStartCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartCustomerMode();
        }

        private void linkLabelStartNormal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartNormalMode();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            LogOffUser();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("ShutDown", "/s /t 0 /f");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HideControlPanel();
        }

        #endregion

        #region Private methods
        private void StartNormalMode()
        {
            try
            {
                if (WorkModeChanger.SetupNormalMode() == 0)
                {
                    WebPlaceSettings.CustomerMode = false;
                    DebugMessageBox("ActivateNormalMode");
                    ApplyModeChanges();
                }
            }
            catch(Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
            }
        }

        private void OpenControlPanel()
        {
            if (DialogResult.OK == ShowLoginWindow())
            {
                ShowControlPanel();
            }
        }

        private void StartCustomerMode()
        {

            try
            {
                if (DialogResult.OK != MessageBox.Show("Switching the mode requires restarting the computer!\n Do you want to restart your computer now?",
                               "Attantion!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    return;

                if (WorkModeChanger.SetupCustomerMode() == 0)
                {
                    WebPlaceSettings.CustomerMode = true;
                    DebugMessageBox("ActivateCustomerMode");
                    ApplyModeChanges();
                }

            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message);
#endif
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
            DebugMessageBox("Debug mode. Exit without reboot.");
#else
            WebPlaceSettings.Save();
            System.Diagnostics.Process.Start("ShutDown", "/r /t 0 /f");
#endif
            this.Close();
        }

        private DialogResult ShowLoginWindow()
        {
            DialogResult DR = DialogResult.Abort;
            if (WebPlaceSettings.PasswordHash == "")
            {
                using (ChangePasswordForm CPF = new Forms.ChangePasswordForm())
                {
                    DR = CPF.ShowDialog();
                }
            }
            else
            {
                using (AuthenticationForm uthenticationForm = new Forms.AuthenticationForm())
                {
                    if (DialogResult.OK == uthenticationForm.ShowDialog())
                    {
                        DR = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Wrong password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            return DR;
        }

        private int ShowChangePasswordWindow()
        {
            using (ChangePasswordForm CPF = new Forms.ChangePasswordForm())
            {
                if (CPF.ShowDialog() == DialogResult.OK)
                    return 0;
                return 1;
            }
        }

        public static void DebugMessageBox(string message)
        {
#if DEBUG
            MessageBox.Show(message);
#endif
        }

        [DllImport("user32.dll")]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        private void LogOffUser()
        {
            canClose = true;
            this.Close();
            ExitWindowsEx(0, 0);
        }
        #endregion

    }
}
