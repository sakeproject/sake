using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sake.Engine;
using Sake.Engine.Builder;
using Sake.Engine.Loader;
using Sake.Engine.Logging;
using Xunit.Extensions;

namespace Sake.Library
{
    public class MakefileTargetsDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
        {
            var makefiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "test-*.shade").Select(Path.GetFileName);
            var loader = new DefaultLoader(new DefaultLog(new SakeSettings { Output = Console.Out }));
            foreach (var makefile in makefiles)
            {
                IBuilder builder;
                try
                {
                    builder = loader.Load(new Options { Makefile = makefile });
                }
                catch (Exception)
                {
                    builder = null;
                }

                if (builder == null)
                {
                    yield return new object[] { makefile, null };
                }
                else
                {
                    foreach (var target in builder.Targets.Values.Where(target => target.Name.StartsWith("test-")))
                    {
                        yield return new object[] { makefile, target.Name };
                    }
                }
            }
        }
    }
}