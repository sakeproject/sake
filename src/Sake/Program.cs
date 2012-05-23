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
            var settings = new SakeSettings
            {
                Output = new ConsoleWriter()
            };

            using (var container = CreateContainer(settings))
            {
                var engine = container.Resolve<SakeEngine>();
                try
                {
                    engine.Execute(args);
                }
                catch (Exception ex)
                {
                    container.Resolve<ILog>().Warn(ex.Message);
                    container.Resolve<ILog>().Verbose("Stack trace: " + Environment.NewLine + ex.StackTrace);
                    Environment.ExitCode = 1;
                }
            }
        }

        static IContainer CreateContainer(ISakeSettings settings)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(settings);
            builder.RegisterType<SakeEngine>();
            builder.RegisterType<DefaultLoader>().As<ILoader>();
            builder.RegisterType<DefaultRunner>().As<IRunner>();
            builder.RegisterType<DefaultLog>().As<ILog>();
            return builder.Build();
        }
    }
}
