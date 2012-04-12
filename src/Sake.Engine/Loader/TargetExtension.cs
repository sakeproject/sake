using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Code;
using Spark.Parser.Markup;

namespace Sake.Engine.Loader
{
    public class TargetExtension : ISparkExtension
    {
        private readonly VisitorContext _context;
        private readonly ElementNode _targetElement;
        private readonly int _targetExtensionCount;
        private readonly AttributeNode _idAttribute;
        private readonly AttributeNode _classAttribute;
        private readonly AttributeNode _descriptionAttribute;
        private AttributeNode _targetAttribute;

        public TargetExtension(VisitorContext context, ElementNode targetElement, int targetExtensionCount)
        {
            _context = context;
            _targetElement = targetElement;
            _targetExtensionCount = targetExtensionCount;
            _idAttribute = _targetElement.Attributes.SingleOrDefault(attr => attr.Name == "id");
            _classAttribute = _targetElement.Attributes.SingleOrDefault(attr => attr.Name == "class");
            _descriptionAttribute = _targetElement.Attributes.SingleOrDefault(attr => attr.Name == "description");
            _targetAttribute = _targetElement.Attributes.SingleOrDefault(attr => attr.Name == "target");
        }

        public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
        {
            var registerTarget = string.Format(
                @"RegisterTarget(""{0}"", ""{1}"", ""{2}"", __target_{3});",
                _idAttribute.Value,
                _classAttribute != null ? _classAttribute.Value : "",
                _descriptionAttribute != null ? _descriptionAttribute.Value : "",
                _targetExtensionCount);

            if (_targetAttribute != null)
            {
                registerTarget +=
                    Environment.NewLine +
                    string.Format(
                        @"RegisterTarget(""{0}"", ""{1}"", null, null);",
                        _targetAttribute.Value,
                        _idAttribute.Value);
            }

            var beginLambda = string.Format(
                @"__target_{0} = () => {{",
                _targetExtensionCount);
            const string endLambda = "};";

            var startingTarget = string.Format(
                @"StartingTarget(""{0}"");",
                _idAttribute.Value);

            var nameAttribute = new AttributeNode("name", _idAttribute.QuotChar, _idAttribute.Nodes) { OriginalNode = _idAttribute };

            var macroAttributes = _targetElement.Attributes
                .Where(x => x != _idAttribute && x != _classAttribute && x != _descriptionAttribute)
                .Concat(new[] { nameAttribute })
                .ToList();
            var macroElement = new SpecialNode(new ElementNode("macro", macroAttributes, false));

            var onceAttribute = new AttributeNode("once", _idAttribute.QuotChar, _idAttribute.Nodes);
            var testElement = new SpecialNode(new ElementNode("test", new[] { onceAttribute }, false));


            macroElement.Body.Add(testElement);
            testElement.Body = body;
            testElement.Body.Insert(0, new StatementNode(startingTarget));

            visitor.Accept(new StatementNode(beginLambda));
            visitor.Accept(testElement);
            visitor.Accept(new StatementNode(endLambda));
            visitor.Accept(new StatementNode(registerTarget));
        }

        public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
        {
            switch (location)
            {
                case OutputLocation.ClassMembers:
                    output
                        .Append("global::System.Action __target_")
                        .Append(_targetExtensionCount)
                        .AppendLine(";");
                    break;
            }
            visitor.Accept(body);
        }
    }
}