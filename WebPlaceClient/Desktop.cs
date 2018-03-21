using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Desktop
    {
        [DllImport("user32.dll")]
        private static extern IntPtr CreateDesktop(string lpszDesktop,
            IntPtr lpszDevice,
            IntPtr pDevmode,
            int dwFlags,
            long dwDesiredAccess,
            IntPtr lpsa);

        [DllImport("user32.dll")]
        private static extern bool SwitchDesktop(IntPtr hDesktop);

        //[DllImport("kernel32.dll")]
        //private static extern bool CreateProcess(
        //    string lpApplicationName,
        //    string lpCommandLine,
        //    IntPtr lpProcessAttributes,
        //    IntPtr lpThreadAttributes,
        //    bool bInheritHandles,
        //    int dwCreationFlags,
        //    IntPtr lpEnvironment,
        //    string lpCurrentDirectory,
        //    ref STARTUPINFO lpStartupInfo,
        //    ref PROCESS_INFORMATION lpProcessInformation);
    }
}
