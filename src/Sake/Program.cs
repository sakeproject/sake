using System;
using System.Diagnostics;
using System.IO;
using Autofac;
using Sake.Engine;
using Sake.Engine.Loader;
using Sake.Engine.Logging;
using Sake.Engine.Runner;

namespace Sake
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new NudoSettings
            {
                Output = new ConsoleWriter()
            };
            
            using (var container = CreateContainer(settings))
            {
                var engine = container.Resolve<NudoEngine>();
                try
                {
                    engine.Execute(args);
                }
                catch (Exception ex)
                {
                    container.Resolve<ILog>().Warn(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        static IContainer CreateContainer(INudoSettings settings)
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
