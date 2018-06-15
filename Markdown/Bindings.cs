using Markdown;
using Markdown.MarkupRules;
using Markdown.Parsers;
using Markdown.TagsParsers;
using Markdown.Renders;
using Ninject.Modules;
using Ninject;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<ILanguageConverter>().To<DefaultHtmlConverter>();
        Bind<IMarkupRule>().To<BoldRuleWithDoubleAsterisks>();
        Bind<IMarkupRule>().To<BoldRuleWithDoubleUnderscores>();
        Bind<IMarkupRule>().To<InlineCode>();
        Bind<IMarkupRule>().To<CursiveRuleWithSingleAsterisks>();
        Bind<IMarkupRule>().To<CursiveRuleWithSingleUnderscores>();
        Bind<IMarkupRule>().To<Header6>();
        Bind<IMarkupRule>().To<Header6>();
        Bind<IMarkupRule>().To<Header5>();
        Bind<IMarkupRule>().To<Header4>();
        Bind<IMarkupRule>().To<Header3>();
        Bind<IMarkupRule>().To<Header2>();
        Bind<IMarkupRule>().To<Headers>();
        Bind<IMarkupRule>().To<Paragraph>();
        Bind<IMarkupRule>().To<ImageTag>();
        Bind<IMarkupRule>().To<HorizontalRuleWith3Hyphens>();
        Bind<IMarkupRule>().To<HorizontalRuleWith3HyphensSeparatedBySpaces>();
        Bind<IMarkupRule>().To<HorizontalRuleWith3Stars>();
        Bind<IMarkupRule>().To<HorizontalRuleWith3StarsSeparatedBySpaces>();
        Bind<IMarkupRule>().To<Link>();
        Bind<IMarkupRule>().To<MultilineCode>();
        Bind<IMarkupRule>().To<Quotation>();

        Bind<IParser, IInLineParser>().To<EmphasisTagParser>();
        Bind<IParser, IInLineParser>().To<ParagraphTagsParser>();
        Bind<IParser, IInLineParser>().To<SingleMarkupTagsParser>();
        Bind<IParser, IInLineParser>().To<ImageTagParser>();
        Bind<IParser, IInLineParser>().To<LinkTagsParser>();
        Bind<IParser, IMultiLineParser>().To<QuotationParser>();
        Bind<IParser, IMultiLineParser>().To<MultilineCodeTagsParser>();
    }
}