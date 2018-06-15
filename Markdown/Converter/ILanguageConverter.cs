using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.Renders
{
    interface ILanguageConverter
    {
        string ConvertToFormat(string markdown);
    }
}
