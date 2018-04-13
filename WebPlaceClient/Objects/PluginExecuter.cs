using PluginInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebPlaceLouncher.Contollers
{
    class PluginLoader
    {
        public IPlugin LoadPlugin(string assemblyPath)
        {
            if (File.Exists(assemblyPath))
            {
                string assembly = Path.GetFullPath(assemblyPath);
                Assembly ptrAssembly = Assembly.LoadFile(assembly);
                foreach (Type item in ptrAssembly.GetTypes())
                {
                    if (!item.IsClass) continue;
                    if (item.GetInterfaces().Contains(typeof(IPlugin)))
                    {
                        return (IPlugin)Activator.CreateInstance(item);
                    }
                }
                throw new Exception("Invalid DLL, Interface not found!");
            }
            return null;
        }
    }
}
