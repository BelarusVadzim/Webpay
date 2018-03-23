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
        static int Main(string[] args)
        {

            WindowsTuner WT = new WindowsTuner();
            if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "customer":
                    case "c":
                        return  WT.ActivateCustomerMode();
                    case "normal":
                    case "n":
                        return  WT.ActivateNormalMode();
                    default:
                        return -1;
                }
            }
            return WT.ActivateNormalMode();
        }
    }
}
