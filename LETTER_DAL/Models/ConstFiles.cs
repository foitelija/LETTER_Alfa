using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LETTER_DAL.Models
{
    public class ConstFiles
    {
        public static string PathFile = Path.Combine(Environment.CurrentDirectory, @"file\path.txt");
    }
}
