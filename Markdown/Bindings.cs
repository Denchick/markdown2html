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
        Bind<ITextRender>().To<DefaultTextRender>();
        Bind<IMarkupRule>().To<Bold>();
        Bind<IMarkupRule>().To<Code>();
        Bind<IMarkupRule>().To<Cursive>();
        Bind<IMarkupRule>().To<Header6>();
        Bind<IMarkupRule>().To<Header6>();
        Bind<IMarkupRule>().To<Header5>();
        Bind<IMarkupRule>().To<Header4>();
        Bind<IMarkupRule>().To<Header3>();
        Bind<IMarkupRule>().To<Header2>();
        Bind<IMarkupRule>().To<Header1>();
        Bind<IMarkupRule>().To<Paragraph>();
        Bind<IMarkupRule>().To<ImageTag>();

        Bind<IMarkupTagsParser>().To<PairedMarkupTagParser>();
        Bind<IMarkupTagsParser>().To<ParagraphTagsParser>();
        Bind<IMarkupTagsParser>().To<SingleMarkupTagsParser>();
        Bind<IMarkupTagsParser>().To<ImageTagParser>();
    }
}