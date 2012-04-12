using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.CSharp.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace Sake.Engine.Loader
{
    public class FunctionsExtension : ISparkExtension
    {
        private readonly VisitorContext _context;
        private readonly ElementNode _node;

        public FunctionsExtension(VisitorContext context, ElementNode node)
        {
            _context = context;
            _node = node;
        }

        public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
        {
            visitor.Accept(body);
        }

        public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
        {
            if (location == OutputLocation.ClassMembers)
            {
                foreach (var snippet in body.OfType<CodeStatementChunk>().SelectMany(chunk => chunk.Code))
                {
                    snippet.Value = snippet.Value.Replace("@class", "class");
                }
                var source = new SourceWriter(new StringWriter(output));
                var generator = new GeneratedCodeVisitor(source, new Dictionary<string, object>(), NullBehaviour.Strict);
                generator.Accept(body);
            }
        }
    }
}