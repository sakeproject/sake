using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Spark.FileSystem;

namespace Nudo.Engine.Loader
{
    public class ViewFolder : IViewFolder
    {
        private readonly string _currentDirectory;

        public ViewFolder(string currentDirectory)
        {
            _currentDirectory = currentDirectory;
        }

        public bool HasView(string path)
        {
            Debug.WriteLine("HasView " + path);
            return false;
        }

        public IList<string> ListViews(string path)
        {
            Debug.WriteLine("ListViews " + path);
            return new string[0];
        }

        public IViewFile GetViewSource(string path)
        {
            var targetPath = Path.Combine(_currentDirectory, path);
            return new ViewFile(targetPath);
        }
    }
}