using Markdown;
using Markdown.MarkupRules;
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
        Bind<IMarkupRule>().To<Header>();
        Bind<IMarkupRule>().To<Paragraph>();
    }
}