using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fclp;
using Markdown.MarkupRules;
using Markdown.Renders;
using Ninject;

namespace Markdown
{
	class Program
	{
		private static void Main(string[] args)
		{
            // for help https://github.com/fclp/fluent-command-line-parser
            var parser = new FluentCommandLineParser<ApplicationArguments>();

		    parser.Setup(arg => arg.InputFilename)
		        .As('i', "input")
		        .SetDefault(@"..\..\Spec.md");

		    parser.Setup(arg => arg.OutputFilename)
		        .As('o', "output")
		        .SetDefault(@"..\..\Spec.html");

		    var arguments = parser.Parse(args);

		    if (arguments.HasErrors)
		    {
                Console.WriteLine(arguments.ErrorText);
                return;
		    }

            var textFromFile = File.ReadAllText(parser.Object.InputFilename);

		    var kernel = new StandardKernel();
		    kernel.Load(Assembly.GetExecutingAssembly());

		    var render = kernel.Get<ITextRender>();
		    var result = render.RenderToHtml(textFromFile);
		    using (var sw = new StreamWriter(parser.Object.OutputFilename))
            {
                sw.WriteLine(result);
            }
        }
    }
}
