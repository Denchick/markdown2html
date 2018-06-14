namespace Markdown.MarkupRules
{
    public class TagAttribute
    {
        public string Name { get; }
        public string Value { get; }

        public TagAttribute(string value, string name)
        {
            Name = name;
            Value = value;
        }
    }
}
