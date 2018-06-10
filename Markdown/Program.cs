using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Markdown.MarkupRules;
using Markdown.Renders;
using Ninject;

namespace Markdown
{
	class Program
	{
		private static void Main(string[] args)
		{
			var textFromFile = File.ReadAllText(@"..\..\TestImage.md");

		    var kernel = new StandardKernel();
		    kernel.Load(Assembly.GetExecutingAssembly());

		    var render = kernel.Get<ITextRender>();
		    var result = render.RenderToHtml(textFromFile);
		    using (var sw = new StreamWriter(@"..\..\Spec.html"))
            {
                sw.WriteLine(result);
            }


            //manual dependency injection
            //var render = new DefaultTextRender(markupRules);
            //var result = render.RenderToHtml(textFromFile);
            //using (var sw = new StreamWriter(@"..\..\Spec.html"))
            //{
            //	sw.WriteLine(result);
            //}

        }
    }
}
