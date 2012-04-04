using System;
using Nudo.Engine;

namespace Nudo
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new NudoSettings
            {
                Output = Console.Out
            };
            var engine = new NudoEngine(settings);
            engine.Execute(args);
        }
    }
}
