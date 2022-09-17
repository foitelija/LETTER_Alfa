using LETTER_DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LETTER_BLL.Controllers
{
    public class PathController
    {
        public static string GetFilePath()
        {
            string filePath = ConstFiles.PathFile;
            string line;
            string path = string.Empty;

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        path = line;
                    }
                }
            }
            return path;
        }

        public static void SetPath(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(ConstFiles.PathFile, false, Encoding.Default))
            {
                writer.WriteLine(filePath);
            }
        }
    }
}
