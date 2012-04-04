using System;
using Nudo.Engine.Builder;
using Spark;

namespace Nudo.Engine.Loader
{
    public class DefaultLoader
    {
        public IBuilder LoadBuilder(Options options)
        {
            var currentDirectory = Environment.CurrentDirectory;

            var settings = new SparkSettings()
                .SetPageBaseType(typeof(BuilderBase));

            var engine = new SparkViewEngine(settings)
            {
                ViewFolder = new ViewFolder(currentDirectory)
            };

            var descriptor = new SparkViewDescriptor
            {
                Templates = new[] { options.Makefile }
            };

            return (IBuilder)engine.CreateInstance(descriptor);
        }
    }
}
