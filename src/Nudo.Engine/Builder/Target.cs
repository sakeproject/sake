using System;
using System.Collections.Generic;

namespace Nudo.Engine.Builder
{
    public class Target
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<string> Dependencies { get; set; }
        public Action Method { get; set; }
    }
}