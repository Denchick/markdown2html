using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public class ParsedSubline
    {
        public int LeftBorderOfSubline { get; set; }
        public int RightBorderOfSubline { get; set; }
        public IMarkupRule MarkupRule { get; set; }
        
        public ParsedSubline(int leftBorderOfSubline, IMarkupRule rule)
        {
            LeftBorderOfSubline = leftBorderOfSubline;
            MarkupRule = rule;
        }

        public ParsedSubline(int leftBorderOfSubline, int rightBorderOfSubline, IMarkupRule rule)
        {
            LeftBorderOfSubline = leftBorderOfSubline;
            RightBorderOfSubline = rightBorderOfSubline;
            MarkupRule = rule;
        }
    }

}
