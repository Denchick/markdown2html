# markdown2html

Проект по курсу ООП. Очень простой конвертер из Markdown в HTML. [Презентация.](https://docs.google.com/presentation/d/1O4_tOjSfoNdDxrRk1y3vU0s2kgYFG8JB4yA-w2_dx8I/edit?usp=sharing)

## Список терминов

- **MarkupRule** - правило разметки текста.


- **Token** - это сущность, которая хранит в себе информацию о том, какое правило разметки нужно применить к фрагменту исходного текста. Фрагмент задается начальным индексом в тексте и конечным.
- **Parser** - сущность, которая разбивает текст на токены, т.е. находит фрагменты, к которым применяются MarkupRule'ы. 
- **LanguageConverter** - переводит текст с одного языка на другой на основании исходного текста и списка токенов.
- **Emphasis** - парные теги. Например, **жирный текст**, *курсив*, ~~перечеркнутый текст~~, `inline code`.

## Что уже умеет

- Обрабатывать emphasis.
- Конвертировать заголовки разного уровня. Если в конце строки с заголовком стоят закрывающие решетки, то они не отображаются! Например, `# Заголовок #` будет переведен как `<h1>Заголовок</h1>`
- Можно добавить вертикальную черту. Делается это просто: `***`, `* * *`, `- - - `, `---`. 
- Обрабатываются картинки с указанием пути и атрибута `alt`
- Обрабатываются ссылки с указанием пути, текста ссылки и атрибута `alt`.
- Текст разделяется по абзацам (`<p>`).
- Цитаты переводятся в тег `<blockquote>`. Поддерживаются вложенные цитаты. Внутри цитат поддерживается обычная разметка.
- Многострочный `<code>` без выбора языка
- Внутри Markdown можно использовать HTML.


## Что не умеет

Из основных возможностей Markdown, у нас нет:

- Таблиц
- Списков
- Ссылок-сносок
- Картинок-сносок

## Как это работает

> Коротко это работает так: у нас есть список сопоставлений правил разметки Markdown разметке HTML. Мы проходимся по тексту, выделяем в этом тексте начало и конец каждого правила (разбиваем текст на токены), а потом на основании полученной информации, переводим теги markdown в теги html.

Для начала нам как-то нужно было сопоставить разметку из Markdown разметке в HTML. Для описания такой сущности у нас есть интерфейс `IMarkupRule`.

```c#
interface IMarkupRule
{
    string MarkdownTag { get; }
    string HtmlTag { get; }
    bool HaveClosingMarkupTag { get; }
    bool HaveClosingHtmlTag { get; }
    bool HasAttribute { get; }
    bool UseForBlockText { get; set; }
    IEnumerable<TagAttribute> Attributes { get; }
    string TextInsideTag { get; }
}
```

Вот так, например, выглядит реализация этого интерфейса для **жирного текста**:

```c#
class Bold : IMarkupRule
{
	public string MarkdownTag { get; } = "__";
	public string HtmlTag { get; } = "strong";
	public bool HaveClosingMarkupTag { get; } = true;
	public bool HaveClosingHtmlTag { get; } = true;
	public bool HasAttribute { get; } = false;
	public bool UseForBlockText { get; set; } = false;
	public IEnumerable<TagAttribute> Attributes { get; }
	public string TextInsideTag { get; } = "";
}
```

Но, это правило еще не дает нам преобразовывать текст из одного формата в другой, оно закладывает только основы для этого. Чтобы парсить текст, мы выделили такую сущность как `IParser`. Задача парсера - разбить текст на токены (`Token`), чтобы его можно было легко перевести в любой удобный формат.

```c#
public class Token
{
    public int LeftBorderOfSubline { get; set; }
    public int RightBorderOfSubline { get; set; }
	public IMarkupRule MarkupRule { get; set; }
}
```

Изначально мы рассчитывали, что парсеров будет много. Каждый парсер проходится линейно по тексту и ищет текст, попадающий под его шаблон.

В начале было очевидно, что по исходному тексту нужно проходиться построчно. Поэтому родилась следующая идея парсера:

```c#
public interface IParser
{
	IEnumerable<Token> ParseLine(string line);
}
```

Но впоследствии мы столкнулись с проблемой, что некоторые теги могут занимать максимум одну строку (например, выделение текста курсивом). Однако, есть теги, которые занимают несколько строк. Простой пример - цитирование.  Поэтому, нам пришлось дополнить архитектуру наших парсеров. 

```c#
public interface IParser
{
}

public interface IInLineParser : IParser
{
	IEnumerable<Token> ParseLine(string line);
}

public interface IMultiLineParser : IParser
{
	IEnumerable<Token> ParseMultilineText(string multilineText);
}
```

Как вы заметили, интерфейс `IParser` пуст. Это сделано лишь для логического объединения `IInLineParser` и `IMultiLineParser`.

После того, как текст будет разбит на токены, пришла пора его перевести из старого формата(markdown) в новый(html). Поэтому у нас есть такая сущность, как конвертер:

```C#
interface ILanguageConverter
{
	string ConvertToFormat(string markdown);
}
```

И реализацией этого интерфейса является `DefaultHtmlConverter`.

## DI-контейнер

В качестве контейнера мы выбрали Ninject.

```c#
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
```

## Роль каждого человека в команде

У Дениса была хреновая архитектура, но Артур пришел и прокачал ее. А еще он жмет двацатку на турничках.

А на самом деле, вклад в проект был поровну, и большую часть времени мы делали проект вместе. Иногда на 2-х компьютерах, иногда на одном. 

## Точки расширения

1. Из уже написанных парсеров, а их около 10, можно выбрать тот, который вам подходит и просто  написать свое правило(`IMarkupRule`), указать его в `Bindings.cs`. Например, я потратил 5 минут, чтобы добавить поддержку зачеркнутого текста.

2. Для оформления HTML текста можно выбрать свой стиль CSS или же конвертировать вообще без стилей.

3. Если хочется конвертировать текст как-то по-другому или в другой формат (не HTML), то достаточно написать свою реализацию `ILanguageConverter`.

4. Если хочется добавить большую поддержку Markdown (например, реализовать списки или таблицы), то нужно реализовать свой парсер: `IInlineParser` или `IMultiplelineParser`.

## Слоистая архитектура

![sloi](images/sloi.png)

