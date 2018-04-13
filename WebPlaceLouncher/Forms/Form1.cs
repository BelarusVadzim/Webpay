using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebPlaceLouncher.Contollers;

namespace WebPlaceLouncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebPlaceExecuter executer = new WebPlaceExecuter();
            executer.RunWebPLace("WebPlace.exe");
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PluginLoader PL = new PluginLoader();
            var plugin = PL.LoadPlugin("testPlug.dll");
            plugin.Execute(this, 0, 0);
        }
    }
}
