namespace Markdown.Renders
{
    interface ILanguageConverter
    {
        string ConvertToFormat(string markdown);
    }
}
