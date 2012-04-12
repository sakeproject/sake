using System;
using System.Collections.Generic;
using System.IO;

namespace Nudo.Engine
{
    public class Options
    {
        public Options()
        {
            WriteOptionsDescriptions = _ => { };
            Targets = new List<string>();
            ChangeDirectory = new List<string>();
        }

        public bool ShowHelp { get; set; }
        public Action<TextWriter> WriteOptionsDescriptions { get; set; }
        public int Verbose { get; set; }

        public IList<string> ChangeDirectory { get; set; }
        public string Makefile { get; set; }

        public IList<string> Targets { get; set; }
    }
}