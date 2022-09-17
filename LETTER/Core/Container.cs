using Autofac;
using NLog;

namespace LETTER.Core
{
    public class Container
    {
        public static IContainer config() 
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(LogManager.GetLogger("log")).As<ILogger>();

            return builder.Build();

        }

    }
}
