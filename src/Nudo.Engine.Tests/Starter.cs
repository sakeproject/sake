using Autofac;
using Nudo.Engine.Loader;
using Nudo.Engine.Logging;
using Nudo.Engine.Runner;

namespace Nudo.Engine.Tests
{
    public class Starter
    {
        public static IContainer CreateContainer(INudoSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(settings);
            builder.RegisterType<NudoEngine>();
            builder.RegisterType<DefaultLoader>().As<ILoader>();
            builder.RegisterType<DefaultRunner>().As<IRunner>();
            builder.RegisterType<DefaultLog>().As<ILog>();
            return builder.Build();
        }

    }
}
