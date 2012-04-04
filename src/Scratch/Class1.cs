using System;
using System.Collections.Generic;

namespace Scratch
{
    public class Class1 : Class1.IClass2
    {
        interface IClass2
        {
            // ioc everything
            FileListDelegate FileList { get; set; }
        }

        

        public delegate IEnumerable<string> FileListDelegate(string path);
        public FileListDelegate FileList { get; set; }
        public Action nuget_push { get; set; }
        public Action nuget_push2 { get; set; }
        public Action nuget_push_ex { get; set; }
        public Action nuget_push3 { get; set; }

        public void Init()
        {
            Loader.Require<IClass2>("Class2", this);
            {
                var that = new Class2();
                ((IClass2)this).FileList = x => that.FileList(x);
            }
            //FileList = path => Class2.FileList(path);

            nuget_push = () =>
                             {
                                 var files = FileList("world");
                                 nuget_push_ex();
                             };

            nuget_push2 = () =>
                              {
                                  var files = FileList("hello");

                              };

            nuget_push_ex = () =>
                               {
                                   nuget_push2();
                                   nuget_push3();
                               };

            nuget_push3 = () =>
                              {

                              };

        }

    }

    public static class Loader
    {
        public static void Require(string class2)
        {
            throw new NotImplementedException();
        }

        internal static void Require<TImports>(string p, TImports imports)
        {
        }
    }
}
