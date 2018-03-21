using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlace.Objects
{
    static class CurentAppDirectory
    {
        public static string CreateFullPathForFile(string ShortFileName)
        {
            string result = string.Format(@"{0}{1}",
                System.AppDomain.CurrentDomain.BaseDirectory, ShortFileName);
            return result;
        }

    }
}
