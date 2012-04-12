using System;
using Spark;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace Sake.Engine.Loader
{
    public class ExtensionFactory : ISparkExtensionFactory
    {
        private int _TargetExtensionCount;
        public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
        {
            if (string.Equals(node.Name, "div", StringComparison.OrdinalIgnoreCase))
            {
                return new TargetExtension(context, node, ++_TargetExtensionCount);
            } if (string.Equals(node.Name, "functions", StringComparison.OrdinalIgnoreCase))
            {
                return new FunctionsExtension(context, node);
            }
            return null;
        }
    }
}