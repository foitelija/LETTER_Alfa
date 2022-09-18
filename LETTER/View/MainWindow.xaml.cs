using Autofac;
using Autofac.Core;
using LETTER.ViewModel;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;
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
            var wordController = container.Resolve<IWordController>();
            DataContext = new MainViewModel(new DialogFileController(), new RobotController(wordController));
        }

    }
}
