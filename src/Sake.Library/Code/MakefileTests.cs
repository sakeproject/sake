using System;
using Sake.Engine;
using Sake.Engine.Loader;
using Sake.Engine.Logging;
using Sake.Engine.Runner;
using Xunit.Extensions;

namespace Sake.Library
{
    public class MakefileTests
    {
        [Theory, MakefileTargetsData]
        public void MakefileTarget(string makefile, string target)
        {
            var settings = new SakeSettings { Output = new RemoveEscapes(Console.Out) };
            var engine = new SakeEngine(settings, new DefaultLoader(new DefaultLog(settings)), new DefaultRunner());
            engine.Execute(new Options { Makefile = makefile, Targets = new[] { target } });
        }
    }
}
