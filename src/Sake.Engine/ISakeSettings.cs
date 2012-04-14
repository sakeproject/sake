using System.IO;

namespace Sake.Engine
{
    public interface ISakeSettings
    {
        TextWriter Output { get; }
    }
}