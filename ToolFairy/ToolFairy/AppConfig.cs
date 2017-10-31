using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolFairy
{
    [Serializable]
    public class AppConfig
    {
        public static string AppPath = System.IO.Directory.GetCurrentDirectory() + "\\";

        public static string ConfigPath = AppPath + "Config\\";
        
    }
}
