using LETTER_BLL.Interfaces;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LETTER_BLL.Controllers
{
    public class DialogFileController : IDialogFile, INotifyPropertyChanged
    {
        private string filePath = PathController.GetFilePath();
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        #region ON PROPERTY CHANGED
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public bool SaveFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        #endregion
    }
}