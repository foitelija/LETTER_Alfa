using LETTER.Core;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LETTER.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        enum WorkStatus
        {
            Ожидание,
            Работаю,
            Завершил,
            Ошибка
        }

        /*  Commands    */
        IDialogFile dialogService;
        private readonly IRobotController _robotController;
        private string StartText = WorkStatus.Ожидание.ToString();

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

        public string StartupText
        {
            get
            {
                return StartText;
            }
            set
            {
                if(StartText == value)
                {
                    return;
                }
                StartText = value;
                OnPropertyChanged("StartupText");
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
                    try
                    {
                        _robotController.RobotStartReadFile(clientBase);
                        StartupText = WorkStatus.Работаю.ToString();
                    }
                    catch
                    {
                        StartupText = WorkStatus.Ошибка.ToString();
                    }
                    
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
                      catch
                      {
                          StartupText = WorkStatus.Ошибка.ToString();
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
