using System.IO;
using Spark.FileSystem;

namespace Nudo.Engine.Loader
{
    public class ViewFile : IViewFile
    {
        private readonly string _targetPath;

        public ViewFile(string targetPath)
        {
            _targetPath = targetPath;
        }

        public Stream OpenViewStream()
        {
            return File.Open(_targetPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        }

        public long LastModified
        {
            get { return 0; }
        }
    }
}