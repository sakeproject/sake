using System;
using System.Collections.Generic;
using System.IO;

namespace Nudo.Engine
{
    public class Options
    {
        public int Verbose { get; set; }
        public IList<string> Targets { get; set; }
        public bool ShowHelp { get; set; }

        public Action<TextWriter> WriteOptionsDescriptions { get; set; }

        public string Makefile { get; set; }
    }
}