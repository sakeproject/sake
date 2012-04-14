using System.IO;
using System.Linq;
using System.Text;

namespace Sake.Library
{
    public class RemoveEscapes : TextWriter
    {
        private readonly TextWriter _writer;

        public RemoveEscapes(TextWriter writer)
        {
            _writer = writer;
        }

        public override Encoding Encoding
        {
            get { return _writer.Encoding; }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            var sanitized = buffer.Skip(index).Take(count).Where(ch => ch >= ' ' || char.IsWhiteSpace(ch)).ToArray();
            base.Write(sanitized, 0, sanitized.Length);
        }
    }
}