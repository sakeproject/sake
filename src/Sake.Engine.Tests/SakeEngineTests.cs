using System.Diagnostics;
using System.IO;
using Autofac;
using Shouldly;
using Xunit;

namespace Sake.Engine.Tests
{
    public class SakeEngineTests
    {
        private readonly IContainer _container;
        private readonly SakeEngine _engine;
        private readonly StringWriter _writer;
        string Output { get { return _writer.ToString(); } }

        public SakeEngineTests()
        {
            _writer = new StringWriter();
            _container = Starter.CreateContainer(new SakeSettings {Output = _writer});
            _engine = _container.Resolve<SakeEngine>();
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


        [Fact]
        public void ShouldLoadFilesFromIncludeDir()
        {
            _engine.Execute("-C", "Files", "-f", "ShouldLoadFilesFromIncludeDir.shade", "-I", "AnotherFolder");
            Output.ShouldContain("WasFoundInAnotherFolder");
        }


        [Fact]
        public void ShouldUseSingleShadeFileAsDefault()
        {
            _engine.Execute("-C", "Files", "-C", "SingleFile");
            Output.ShouldContain("ShouldUseSingleShadeFileAsDefault");
            Output.ShouldContain("NotThisOne");
            Output.ShouldContain("OrThisOne");
        }
    }
}
