using Nudo.Engine.Builder;

namespace Nudo.Engine.Loader
{
    public interface ILoader
    {
        IBuilder Load(Options options);
    }
}