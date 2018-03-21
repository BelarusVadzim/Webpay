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
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Authenticator authenticator = new Objects.Authenticator();
            if (authenticator.CheckPassword(textBox1.Text))
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
