using System.Collections.Generic;

namespace Nudo.Engine.Builder
{
    public interface IBuilder
    {
        IDictionary<string, Target> Targets { get; }
        string DefaultTarget { get; set; }
        void CallTarget(string target);
    }
}