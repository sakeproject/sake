using Xunit;

namespace Nudo.Engine.Tests
{
    public class NudoEngineTests
    {
        [Fact]
        public void EngineCanBeCreated()
        {
            var engine = new NudoEngine(new NudoSettings());
        }
    }
}
