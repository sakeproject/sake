using Nudo.Engine;

namespace Nudo
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new NudoEngine(new NudoSettings());
            engine.Execute(args);
        }
    }
}
