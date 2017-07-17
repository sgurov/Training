using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineTask
{
    public static class FileHandler
    {
        public static string[] ReadInputData(string path)
        {
            string[] result;

            if (File.Exists(path))
            {
                result = File.ReadAllLines(path);
                return result;
            }
            else
            {
                ConsolePrinter.PrintLine("File doesn't exist!");
            }

            return null;
        }
    }
}
