using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LETTER.ViewModel;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;

namespace LETTER.Core
{
    public class SetFilePathCommand : ICommand
    {
        private readonly IDialogFile _dialogFile;

        public SetFilePathCommand(IDialogFile dialogFile)
        {
            _dialogFile = dialogFile;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            try
            {
                if(_dialogFile.OpenFileDialog())
                {
                    MessageBox.Show("открылся");
                }
                else
                {
                    MessageBox.Show("не открылся");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }
    }
}
