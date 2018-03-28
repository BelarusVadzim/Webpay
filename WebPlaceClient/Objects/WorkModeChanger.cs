using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlace.Objects
{
    static class WorkModeChanger
    {
        public static int SetupNormalMode()
        {
           return Run("normal");
        }

        public static int SetupCustomerMode()
        {
            return Run("customer");
        }

        private static int Run(string Arguments)
        {
            ProgramExecuter exec = new ProgramExecuter();
            exec.ErrorExecuteProgram += (sender, message) => ErrorChangeMode?.Invoke(message);
            return exec.ExecuteProgramWithElevation(CurentAppDirectory.CreateFullPathForFile("ChangeWorkMode.exe"), Arguments);

        }

        public static event Action<string> ErrorChangeMode;
    }
}
