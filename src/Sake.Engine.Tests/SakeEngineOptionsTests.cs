using Autofac;
using Shouldly;
using Xunit;

namespace Sake.Engine.Tests
{
    public class SakeEngineOptionsTests
    {
        private readonly IContainer _container;
        private readonly SakeEngine _engine;

        public SakeEngineOptionsTests()
        {
            _container = Starter.CreateContainer(new SakeSettings());
            _engine = _container.Resolve<SakeEngine>();
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