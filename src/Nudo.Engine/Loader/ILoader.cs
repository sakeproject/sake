using Sake.Engine.Builder;

namespace Sake.Engine.Loader
{
    public interface ILoader
    {
        IBuilder Load(Options options);
    }
}