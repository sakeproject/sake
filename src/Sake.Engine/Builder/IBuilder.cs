using System.Collections.Generic;

namespace Sake.Engine.Builder
{
    public interface IBuilder
    {
        IDictionary<string, Target> Targets { get; }
        string DefaultTarget { get; set; }
        void CallTarget(string target);
    }
}