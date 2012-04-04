using Spark;

namespace Nudo.Engine.Builder
{
    public abstract class BuilderBase<TModel> : SparkViewBase, IBuilder
    {
        protected object HTML(object value)
        {
            return value;
        }
    }

    public abstract class BuilderBase : BuilderBase<object>
    {
    }
}
