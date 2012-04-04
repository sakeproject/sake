using System;
using System.IO;
using Nudo.Engine.Loader;
using Shouldly;
using Xunit;

namespace Nudo.Engine.Tests.Loader
{
    public class DefaultLoaderTests
    {
        private readonly DefaultLoader _loader;

        public DefaultLoaderTests()
        {
            _loader = new DefaultLoader();
        }

        [Fact]
        public void ShouldLoadSparkMakefiles()
        {
            var options = new Options
            {
                Makefile = Path.Combine("Loader", "Files", "makefile.spark")
            };
            var builder = _loader.LoadBuilder(options);
            builder.ShouldNotBe(null);
        }

        [Fact]
        public void ShouldFailIfFileNotFound()
        {
            var options = new Options
            {
                Makefile = Path.Combine("Loader", "Files", "no-such-file.spark")
            };
            Should.Throw<Exception>(() => _loader.LoadBuilder(options));           
        }
    }
}
