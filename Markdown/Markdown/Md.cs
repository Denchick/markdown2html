using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using NUnit.Framework;
using System.Text;
using FluentAssertions;

namespace Markdown
{
	public class Md
	{
		private List<IMarkupRule> CurrentMarkupRules { get; }
			
		public Md(IEnumerable<IMarkupRule> rules)
		{
			CurrentMarkupRules = rules
				.Where(e => e?.MarkupTag != null)
				.OrderBy(e => e.MarkupTag.Length)
				.ToList();
		}

		public string RenderToHtml(string markdown)
		{
		    var result = new StringBuilder();
			var parser = new TextParser(CurrentMarkupRules);
			var render = new TextRender(CurrentMarkupRules);
		    foreach (var line in markdown.Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries))
		    {
			    var parsed = parser.ParseLine(line);
			    var rendered = render.RenderLine(line, parsed);
		        result.Append($"{rendered}\n");
		    }
		    return result.ToString();
		}


	}
}