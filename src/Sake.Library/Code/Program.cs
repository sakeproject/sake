using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace Sake.Library
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlNode results;
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineOnAttributes = true,
                NewLineHandling = NewLineHandling.Replace,
                NewLineChars = Environment.NewLine,
            };
            using (var writer = XmlWriter.Create(Console.Out, settings))
            {
                var executor = new Xunit.ExecutorWrapper(
                    typeof(Program).Assembly.Location,
                    AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
                    false);

                results = executor.RunAssembly(node => true);
                results.WriteTo(writer);
            }
            Console.WriteLine();

            var failed = results.Attributes["failed"].Value;
            if (failed != "0")
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} Failed", failed);
                foreach (var failure in results.SelectNodes("//failure/message").OfType<XmlElement>())
                {
                    Console.WriteLine(failure.ParentNode.ParentNode.Attributes["name"].Value);
                    Console.WriteLine(failure.InnerText);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
            }
        }
    }
}
