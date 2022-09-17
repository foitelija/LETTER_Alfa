using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LETTER_BLL.Interfaces
{
    public interface IDialogFile
    {
        string FilePath { get; set; }
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }
}
