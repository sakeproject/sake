using System;
using System.IO;
using System.Text;
using Sake.Engine.Builder;
using Sake.Engine.Logging;
using Spark;
using Spark.FileSystem;

namespace Sake.Engine.Loader
{
    public class DefaultLoader : ILoader
    {
        private readonly ILog _log;

        public DefaultLoader(ILog log)
        {
            _log = log;
        }

        public IBuilder Load(Options options)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var assemblyDirectory = Path.GetDirectoryName(typeof(SakeEngine).Assembly.Location);

            var settings = new SparkSettings()
                .SetPageBaseType(typeof(BuilderBase))
                .SetAutomaticEncoding(true)
                .SetAttributeBehaviour(AttributeBehaviour.TextOriented)
                .SetDebug(true);

            IViewFolder viewFolder = new FileSystemViewFolder(currentDirectory);
            foreach(var includeDir in options.IncludeDirectory)
            {
                viewFolder = new CombinedViewFolder(viewFolder, new FileSystemViewFolder(Path.Combine(currentDirectory, includeDir)));
            }
            viewFolder = new CombinedViewFolder(viewFolder, new FileSystemViewFolder(assemblyDirectory));

            var engine = new SparkViewEngine(settings)
                               {
                                   ViewFolder = viewFolder,
                                   ExtensionFactory = new ExtensionFactory(),
                               };

            var descriptor = new SparkViewDescriptor
            {
                Templates = new[] { options.Makefile }
            };

            var builder = (BuilderBase)engine.CreateInstance(descriptor);
            builder.Output = new StringWriter();
            builder.Log = _log;
            builder.Render();
            return builder;
        }
    }
}
