using System;
using System.IO;

namespace Nudo.Engine
{
    public class NudoSettings : INudoSettings
    {
        public TextWriter Output { get; set; }
    }
}