using Autofac;
using LETTER_BLL.Controllers;
using LETTER_BLL.Interfaces;
using NLog;

namespace LETTER.Core
{
    public class Container
    {
        public static IContainer config() 
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(LogManager.GetLogger("log")).As<ILogger>();
            builder.RegisterType<RobotController>().As<IRobotController>();
            builder.RegisterType<DataConversionController>().As<IDataConversionController>();
            builder.RegisterType<WordController>().As<IWordController>();

            return builder.Build();

        }

    }
}
