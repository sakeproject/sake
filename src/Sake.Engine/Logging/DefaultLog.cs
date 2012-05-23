using System;

namespace Sake.Engine.Logging
{
    public class DefaultLog : ILog
    {
        private ISakeSettings _settings;

        public DefaultLog(ISakeSettings settings)
        {
            _settings = settings;
        }

        public void Info(object value)
        {
            _settings.Output.WriteLine("\x1b-\x02info\x1b-\x07: {0}", value);
        }

        public void Warn(object value)
        {
            _settings.Output.WriteLine("\x1b-\x0ewarn\x1b-\x07: {0}", value);
        }

        public void Verbose(object value)
        {
            _settings.Output.WriteLine("\x1b-\x03verbose\x1b-\x07: {0}", value);
        }
    }
}
