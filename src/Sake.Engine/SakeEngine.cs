using System.IO;
using System.Linq;
using NDesk.Options;
using Sake.Engine.Loader;
using Sake.Engine.Runner;

namespace Sake.Engine
{
    public class SakeEngine
    {
        private readonly ISakeSettings _settings;
        private readonly ILoader _loader;
        private readonly IRunner _runner;

        public SakeEngine(ISakeSettings settings, ILoader loader, IRunner runner)
        {
            _settings = settings;
            _loader = loader;
            _runner = runner;
        }

        public void Execute(params string[] args)
        {
            try
            {
                Execute(Parse(args));
            }
            catch (OptionException ex)
            {
                _settings.Output.Write("Sake: ");
                _settings.Output.WriteLine(ex.Message);
                _settings.Output.WriteLine("Try 'Sake --help' for more information.");
            }
        }

        public void Execute(Options options)
        {
            if (options.ShowHelp)
            {
                ShowHelp(options);
                return;
            }

            if (string.IsNullOrEmpty(options.Makefile))
            {
                options.Makefile = "makefile.shade";
            }

            var originalDirectory = Directory.GetCurrentDirectory();
            try
            {
                foreach (var changeDirectory in options.ChangeDirectory)
                {
                    Directory.SetCurrentDirectory(changeDirectory);
                }

                var builder = _loader.Load(options);

                var targets = options.Targets;
                if (targets == null || !targets.Any())
                {
                    targets = new[] { builder.DefaultTarget };
                }

                foreach (var target in targets)
                {
                    builder.CallTarget(target);
                }
            }
            finally
            {
                Directory.SetCurrentDirectory(originalDirectory);
            }
        }

        public Options Parse(params string[] args)
        {
            var options = new Options();

            var optionSet = new OptionSet()
                .Add("v|verbose", "increase verbosity", v => ++options.Verbose)
                .Add("h|?|help", "show this message and exit", v => options.ShowHelp = v != null)
                .Add("f|file|makefile=", "read file as a makefile", v => options.Makefile = v)
                .Add("C|directory=", "change current directory", v => options.ChangeDirectory.Add(v))
                .Add("I|include-dir=", "specifies a directory to search for included files", v => options.IncludeDirectory.Add(v))
                ;

            options.Targets = optionSet.Parse(args);
            options.WriteOptionsDescriptions = optionSet.WriteOptionDescriptions;
            return options;
        }

        public void ShowHelp(Options options)
        {
            _settings.Output.WriteLine("Usage: Sake [OPTIONS]+ [target]+");
            _settings.Output.WriteLine();
            _settings.Output.WriteLine("Options:");
            options.WriteOptionsDescriptions(_settings.Output);
        }

    }
}
