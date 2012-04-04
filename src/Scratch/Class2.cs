using System;
using System.Collections.Generic;

namespace Scratch
{
    public class Class2
    {
        // Require("x");

        public delegate IEnumerable<string> FileListDelegate(string path);

        public FileListDelegate FileList;


        public void Execute()
        {
            FileList = path =>
                           {
                               yield return "hello";
                           };
        }
    }
}
