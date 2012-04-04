using NDesk.Options;

namespace Nudo.Engine
{
    public class NudoEngine
    {
        private readonly INudoSettings _settings;

        public NudoEngine(INudoSettings settings)
        {
            _settings = settings;
        }

        public void Execute(params string[] args)
        {
            try
            {
                Execute(Parse(args));
            }
            catch (OptionException ex)
            {
                _settings.Output.Write("Nudo: ");
                _settings.Output.WriteLine(ex.Message);
                _settings.Output.WriteLine("Try 'Nudo --help' for more information.");
            }
        }

        public void Execute(Options options)
        {
            if (options.ShowHelp)
            {
                ShowHelp(options);
                return;
            }
        }

        public Options Parse(params string[] args)
        {
            var options = new Options();

            var optionSet = new OptionSet()
                .Add("v|verbose", "increase verbosity", v => ++options.Verbose)
                .Add("h|?|help", "show this message and exit", v => options.ShowHelp = v != null)
                .Add("f|file|makefile=", "read file as a makefile", v => options.Makefile = v);

            options.Targets = optionSet.Parse(args);
            options.WriteOptionsDescriptions = optionSet.WriteOptionDescriptions;
            return options;
        }

        public void ShowHelp(Options options)
        {
            _settings.Output.WriteLine("Usage: Nudo [OPTIONS]+ [target]+");
            _settings.Output.WriteLine();
            _settings.Output.WriteLine("Options:");
            options.WriteOptionsDescriptions(_settings.Output);
        }

    }
}
