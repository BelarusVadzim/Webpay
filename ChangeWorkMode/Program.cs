using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeWorkMode
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowsTuner WT = new WindowsTuner();
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "customer":
                        WT.ActivateCustomerMode();
                        break;
                    case "normal":
                    default:
                        WT.ActivateNormalMode();
                        break;
                }
                return;
            }
            WT.ActivateNormalMode();
        }
    }
}
