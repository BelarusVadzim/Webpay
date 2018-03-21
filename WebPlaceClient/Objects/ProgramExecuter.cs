using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlace.Objects
{
    class ProgramExecuter
    {
        public void ExecuteProgramWithElevation(string FileName, string Arguments)
        {
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

            ProcessStartInfo info = new ProcessStartInfo(FileName);
            info.UseShellExecute = true;
            info.Verb = "runas";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Arguments = Arguments;
            try
            {
                Process.Start(info);
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED)
                    ErrorExecuteProgram?.Invoke(this, "Cancelled");
                else
                    ErrorExecuteProgram?.Invoke(this, ex.Message);
            }
        }

        public event Action<object, string> ErrorExecuteProgram;
    }
}
