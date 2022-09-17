using LETTER.Core;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace LETTER.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        /*  Commands    */
        IDialogFile dialogService;


        public MainViewModel(IDialogFile dialogService)
        {
            this.dialogService = dialogService;
        }

        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              PathController.SetPath(dialogService.FilePath);
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }



        public RelayCommand MoveWindowCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Application.Current.MainWindow.DragMove();
                });
            }
        }
        public RelayCommand ShutdownWindowCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Application.Current.Shutdown();
                });
            }
        }
    }
}
