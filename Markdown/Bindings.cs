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
        Bind<ILanguageConverter>().To<DefaultHtmlConverter>().InSingletonScope();
        Bind<IMarkupRule>().To<BoldRuleWithDoubleAsterisks>().InSingletonScope();
        Bind<IMarkupRule>().To<BoldRuleWithDoubleUnderscores>().InSingletonScope();
        Bind<IMarkupRule>().To<InlineCode>().InSingletonScope();
        Bind<IMarkupRule>().To<CursiveRuleWithSingleAsterisks>().InSingletonScope();
        Bind<IMarkupRule>().To<CursiveRuleWithSingleUnderscores>().InSingletonScope();
        Bind<IMarkupRule>().To<Strike>().InSingletonScope();
        Bind<IMarkupRule>().To<Header6>().InSingletonScope();
        Bind<IMarkupRule>().To<Header6>().InSingletonScope();
        Bind<IMarkupRule>().To<Header5>().InSingletonScope();
        Bind<IMarkupRule>().To<Header4>().InSingletonScope();
        Bind<IMarkupRule>().To<Header3>().InSingletonScope();
        Bind<IMarkupRule>().To<Header2>().InSingletonScope();
        Bind<IMarkupRule>().To<Headers>().InSingletonScope();
        Bind<IMarkupRule>().To<Paragraph>().InSingletonScope();
        Bind<IMarkupRule>().To<ImageTag>().InSingletonScope();
        Bind<IMarkupRule>().To<HorizontalRuleWith3Hyphens>().InSingletonScope();
        Bind<IMarkupRule>().To<HorizontalRuleWith3HyphensSeparatedBySpaces>().InSingletonScope();
        Bind<IMarkupRule>().To<HorizontalRuleWith3Stars>().InSingletonScope();
        Bind<IMarkupRule>().To<HorizontalRuleWith3StarsSeparatedBySpaces>().InSingletonScope();
        Bind<IMarkupRule>().To<Link>().InSingletonScope();
        Bind<IMarkupRule>().To<MultilineCode>().InSingletonScope();
        Bind<IMarkupRule>().To<Quotation>().InSingletonScope();

        Bind<IParser, IInLineParser>().To<EmphasisTagParser>();
        Bind<IParser, IInLineParser>().To<ParagraphTagsParser>();
        Bind<IParser, IInLineParser>().To<SingleMarkupTagsParser>();
        Bind<IParser, IInLineParser>().To<ImageTagParser>();
        Bind<IParser, IInLineParser>().To<LinkTagsParser>();
        Bind<IParser, IMultiLineParser>().To<QuotationParser>();
        Bind<IParser, IMultiLineParser>().To<MultilineCodeTagsParser>();
    }
}