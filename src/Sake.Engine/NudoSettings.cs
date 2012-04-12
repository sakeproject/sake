using System;
using System.IO;

namespace Sake.Engine
{
    public class NudoSettings : INudoSettings
    {
        public TextWriter Output { get; set; }
    }
}