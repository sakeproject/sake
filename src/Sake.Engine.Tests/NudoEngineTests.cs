using System.Diagnostics;
using System.IO;
using Autofac;
using Shouldly;
using Xunit;

namespace Sake.Engine.Tests
{
    public class NudoEngineTests
    {
        private readonly IContainer _container;
        private readonly NudoEngine _engine;
        private readonly StringWriter _writer;
        string Output { get { return _writer.ToString(); } }

        public NudoEngineTests()
        {
            _writer = new StringWriter();
            _container = Starter.CreateContainer(new NudoSettings {Output = _writer});
            _engine = _container.Resolve<NudoEngine>();
        }

        [Fact]
        public void ShouldBeCreated()
        {            
            _engine.ShouldNotBe(null);
        }

        [Fact]
        public void ShouldLoadMakefileSparkFromCurrentDirectoryByDefault()
        {
            _engine.Execute();
            Output.ShouldContain("ExecDefaultTask");
            Output.ShouldContain("ExecAnotherTask");
            Output.ShouldContain("ExecYetAnotherTask");
        }

        [Fact]
        public void ShouldRunNamedTarget()
        {
            _engine.Execute("YetAnother");
            Output.ShouldNotContain("ExecDefaultTask");
            Output.ShouldNotContain("ExecAnotherTask");
            Output.ShouldContain("ExecYetAnotherTask");
        }

        [Fact]
        public void ShouldLoadDifferentFileIfProvided()
        {
            _engine.Execute("-f", "different.shade");
            Output.ShouldContain("ExecDifferentFile");
        }

        [Fact]
        public void ShouldChangeCurrentDirectoryBeforeLoadingFile()
        {
            _engine.Execute("-C", "Files");
            Output.ShouldContain("This is Files\\makefile.shade");
        }

        [Fact]
        public void ShouldFindPartialFilesInCurrentDirectoryOrSharedSubdirectory()
        {
            _engine.Execute("-C", "Files", "-f", "ShouldFindPartialFilesInCurrentDirectoryOrSharedSubdirectory.shade");
            Output.ShouldContain("WasFoundInCurrentFolder");
            Output.ShouldContain("WasFoundInSharedFolder");
        }


        [Fact]
        public void ShouldAddTargetAsDependencyToNamedTarget()
        {
            _engine.Execute("-C", "Files", "-f", "ShouldAddTargetAsDependencyToNamedTarget.shade");
            Output.ShouldContain("Compile");
            Output.ShouldContain("HasBeenAdded");
        }
    }
}
