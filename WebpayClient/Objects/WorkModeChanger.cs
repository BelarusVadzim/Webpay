using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Objects
{
    static class WorkModeChanger
    {
        public static void SetupNormalMode()
        {
            Run("normal");
        }

        public static void SetupCustomerMode()
        {
            Run("customer");
        }

        private static void Run(string Arguments)
        {
            ProgramExecuter exec = new ProgramExecuter();
            exec.ErrorExecuteProgram += (m, n) => ErrorChangeMode(n);
            exec.ExecuteProgramWithElevation(System.AppDomain.CurrentDomain.BaseDirectory + "ChangeWorkMode.exe", Arguments);
        }

        public static event Action<string> ErrorChangeMode;

    }
}
