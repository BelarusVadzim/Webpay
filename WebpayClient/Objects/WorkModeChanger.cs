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
            DeactivateCustomerMode?.Invoke();
        }

        public static void SetupCustomerMode()
        {
            Run("customer");
            ActivateCustomerMode?.Invoke();
        }

        private static void Run(string Arguments)
        {
            ProgramExecuter exec = new ProgramExecuter();
            exec.ErrorExecuteProgram += (m, n) => ErrorChangeMode(n);
            exec.ExecuteProgramWithElevation(CurentAppDirectory.CreateFullPathForFile("ChangeWorkMode.exe"), Arguments);
        }

        public static event Action<string> ErrorChangeMode;
        public static event Action ActivateCustomerMode;
        public static event Action DeactivateCustomerMode;
    }
}
