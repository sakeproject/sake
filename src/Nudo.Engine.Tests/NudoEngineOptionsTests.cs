using Autofac;
using Shouldly;
using Xunit;

namespace Nudo.Engine.Tests
{
    public class NudoEngineOptionsTests
    {
        private readonly IContainer _container;
        private readonly NudoEngine _engine;

        public NudoEngineOptionsTests()
        {
            _container = Starter.CreateContainer(new NudoSettings());
            _engine = _container.Resolve<NudoEngine>();
        }

        [Fact]
        public void VerboseIsRecognized()
        {
            _engine.Parse("-v").Verbose.ShouldBe(1);
            _engine.Parse("--verbose").Verbose.ShouldBe(1);
        }

        [Fact]
        public void MoreThanOneVerboseMayBePassed()
        {
            _engine.Parse("-vv").Verbose.ShouldBe(2);
            _engine.Parse("-v", "-v").Verbose.ShouldBe(2);
        }

        [Fact]
        public void PlainOldArgsBecomeTargets()
        {
            _engine.Parse("this", "is", "a", "test").Targets.ShouldBe(new[] { "this", "is", "a", "test" });
        }

        [Fact]
        public void MakeFileMayBePassedIn()
        {
            var options = _engine.Parse("--file", "hello.txt", "foo");
            options.Targets.ShouldBe(new[] { "foo" });
            options.Makefile.ShouldBe("hello.txt");
        }

        [Fact]
        public void CurrentDirectoryMayBeProvided()
        {
            _engine.Parse("-C", "build").ChangeDirectory.ShouldBe(new[] { "build" });
            _engine.Parse("--directory", "build").ChangeDirectory.ShouldBe(new[] { "build" });
            _engine.Parse("--directory", "build", "-C", "src").ChangeDirectory.ShouldBe(new[] { "build", "src" });
        }
    }
}