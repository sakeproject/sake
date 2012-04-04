using Shouldly;
using Xunit;

namespace Nudo.Engine.Tests
{
    public class NudoEngineTests
    {
        [Fact]
        public void EngineCanBeCreated()
        {
            var engine = new NudoEngine(new NudoSettings());
            engine.ShouldNotBe(null);
        }

        [Fact]
        public void EngineWillLoadMakefileSparkFromCurrentDirectoryByDefault()
        {
            var engine = new NudoEngine(new NudoSettings());
            engine.Execute();
        }
    }
}
