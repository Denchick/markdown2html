using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Markdown.MarkupRules;

namespace Markdown
{
	class Program
	{
		private static void Main(string[] args)
		{
			var textFromFile = File.ReadAllText(@"..\..\Spec.md");
			var markupRules = Utils.GetAllAvailableRules();
			var md = new Md(markupRules);
			var result = md.RenderToHtml(textFromFile);
			
			using (var sw = new StreamWriter(@"..\..\Spec.html"))
			{
				sw.WriteLine(result);
			}
			
		}
	}
}
