using Autofac;
using Autofac.Core;
using LETTER.ViewModel;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;
using NLog;
using System.Windows;

namespace LETTER
{
    public partial class MainWindow : Window
    {
        private readonly IContainer container;
        public MainWindow()
        {
            container = Core.Container.config();
            InitializeComponent();
            var converter = container.Resolve<IDataConversionController>();
            var logger = container.Resolve<ILogger>();
            logger.Info(" ");
            logger.Info("Запуск приложения");
            DataContext = new MainViewModel(new DialogFileController(), new RobotController(converter, logger));
        }

    }
}
