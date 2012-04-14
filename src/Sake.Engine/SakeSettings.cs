using System;
using System.IO;

namespace Sake.Engine
{
    public class SakeSettings : ISakeSettings
    {
        public TextWriter Output { get; set; }
    }
}