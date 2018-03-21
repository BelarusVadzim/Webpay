using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebPlace.Objects;

namespace WebPlace.Forms
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
            if(WebPlaceSettings.FirstBoot)
            {
                textBoxCurrentPassword.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PasswordChanger PwdChgr = new PasswordChanger();
            switch (PwdChgr.Change(textBoxCurrentPassword.Text, textBoxNewPassword.Text, textBoxConfirmNewPassword.Text))
            {
                case  PasswordChangeResult.OK:
                    OnPasswordChangeOK();
                    break;
                case PasswordChangeResult.CurrentPassworFail:
                    OnCurrentPassworFail();
                    break;

                case PasswordChangeResult.ConfirmNewPasswordCompareFail:
                    OnConfirmNewPasswordErrorFail();
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        private void OnPasswordChangeOK()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

            
        private void OnCurrentPassworFail()
        {
            MessageBox.Show("Incorrect current password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnConfirmNewPasswordErrorFail()
        {
            MessageBox.Show("The entered password does not match the confirmation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
