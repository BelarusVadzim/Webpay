using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testPlug
{
    public class Class1 : IPlugin
    {
        public string Name => throw new NotImplementedException();

        public double GetLastResult => throw new NotImplementedException();

        public event EventHandler OnExecute;

        public void ExceptionTest(string input)
        {
            throw new NotImplementedException();
        }

        public double Execute(double value1, double value2)
        {
            return 0;
        }

        public double Execute(IWin32Window ParentWindow, double value1, double value2)
        {
            Form1 F = new Form1();
            F.ShowDialog(ParentWindow);
            return 0;
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }
    }
}
