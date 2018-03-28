using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebPlace.Objects
{
    class ProgramExecuter
    {

        public int ExecuteProgramWithElevation(string FileFullName, string Arguments)
        {
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

            ProcessStartInfo info = new ProcessStartInfo(FileFullName);
            info.UseShellExecute = true;
            info.Verb = "runas";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Arguments = Arguments;
            int exitCode = 0;
            try
            {
                Process p = Process.Start(info);
                p.Exited += (m, n) => exitCode = ((Process)m).ExitCode;
                switch (exitCode)
                {
                    case 0:
                        return 0;
                    default:
                        return 3;
                }
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED)
                {
                    ErrorExecuteProgram?.Invoke(this, "Cancelled");
                    return 1;
                }
                else
                {
                    ErrorExecuteProgram?.Invoke(this, String.Format("Error launch ChangeWorkMode.exe. \n {0}", ex.Message));
                    return 2;
                }
            }
        }
        public event Action<object, string> ErrorExecuteProgram;
    }
}
