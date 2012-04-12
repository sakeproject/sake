using System;

namespace Nudo.Engine.Logging
{
    public class DefaultLog : ILog
    {
        private INudoSettings _settings;

        public DefaultLog(INudoSettings settings)
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
    }
}
