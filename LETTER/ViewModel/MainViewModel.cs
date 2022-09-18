using LETTER.Core;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LETTER.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        /*  Commands    */
        IDialogFile dialogService;
        private readonly IRobotController _robotController;

        public MainViewModel(IDialogFile dialogService, IRobotController robotController)
        {
            this.dialogService = dialogService;
            _robotController = robotController;
        }


        public  IDialogFile DialogFileCommand
        {
            get { return dialogService; }
            set
            {
                dialogService = value;
            }
        }
        public IDialogFile DialogFileService
        {
            get { return this.dialogService; }
            set
            {
                dialogService = value;
            }
        }

        private string clientBase;
        public string ClientBase
        {
            get { return clientBase; }
            set
            {
                clientBase = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand startCommad;
        public RelayCommand StartCommad
        {
            get
            {
                return startCommad ?? new RelayCommand(obj =>
                {
                    _robotController.RobotStartReadFile(clientBase);
                });
            }
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
