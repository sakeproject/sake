using Autofac;
using Sake.Engine.Loader;
using Sake.Engine.Logging;
using Sake.Engine.Runner;

namespace Sake.Engine.Tests
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
