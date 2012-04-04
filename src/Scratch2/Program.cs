using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Spark;
using Spark.FileSystem;

namespace Scratch2
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new SparkSettings()
                .SetPageBaseType(typeof(BuilderBase))
                .SetDebug(true);

            var engine = new SparkViewEngine(settings);
            var builder = engine.CreateInstance(
                new SparkViewDescriptor
                    {
                        Templates = new[] {"build.spark"}
                    });
            
            builder.RenderView(Console.Out);

            var result = CallMethod(builder, "test");
            Console.Write(result);

        }

        private static object CallMethod(object obj, string name)
        {
            
            var methodInfo = obj.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
            return methodInfo.Invoke(obj, new object[0]);
        }
    }

    public abstract class BuilderBase : SparkViewBase
    {
        public Func<object> target(Func<object> method, params Func<object>[] dependencies)
        {
            return method;
        }

        public string HTML(object value)
        {
            return value.ToString();
        }
    }
}
