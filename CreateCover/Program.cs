using ArgsParser;
using CreateCover.Models;

namespace CreateCover;

// See:
// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-width
// https://webdesign.tutsplus.com/how-to-hand-code-svg--cms-30368t

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("CREATE COVER");
        Console.WriteLine("Generate themed 900x1350 pixel PNG and SVG book covers");
        Console.WriteLine();

        // Define and show command arguments.
        Console.WriteLine("OPTIONS");
        Console.WriteLine();
        var defaultTheme = Theme.GetStandardTheme("default");
        var parser = new Parser(args)
            .RequiresOption<string>("file", "where to write the output", "covers.html")
            .RequiresOption<string>("title", "the book title (eg \"The | Fellowship | of the | Ring\")")
            .SupportsOption<string>("subtitle", "the book subtitle (eg titles in a box set)")
            .RequiresOption<string>("author", "the book author (eg \"JRR Tolkien\")")
            .RequiresOption<string>("series", "the book series (eg \"The Lord of the Rings 1\")")
            .SupportsOption<string>("titlefont", "title font name,pixels", defaultTheme.TitleFont.ToString())
            .SupportsOption<string>("subtitlefont", "subtitle font,pixels", defaultTheme.SubtitleFont.ToString())
            .SupportsOption<string>("authorfont", "author font,pixels", defaultTheme.AuthorFont.ToString())
            .SupportsOption<string>("seriesfont", "series font,pixels", defaultTheme.SeriesFont.ToString())
            .SupportsFlag("scaleauthor", "scale author name to fit its area")
            .SupportsFlag("scaleseries", "scale series name to fit its area")
            .SupportsFlag("debug", "show extra debugging info")
            .AddCustomOptionValidator("file", (string key, object filename) =>
            {
                var errs = new List<string>();
                var ext = Path.GetExtension($"{filename}").ToLowerInvariant();
                if (ext != ".html") errs.Add($"Not an HTML filename: {filename}");
                return errs;
            })
            .AddCustomOptionValidator("titlefont", CheckFont)
            .AddCustomOptionValidator("subtitlefont", CheckFont)
            .AddCustomOptionValidator("authorfont", CheckFont)
            .AddCustomOptionValidator("seriesfont", CheckFont)
        ;
        parser.Help(2);

        // Load and validate.
        parser.Parse();
        if (parser.HasErrors)
        {
            Console.WriteLine("ERRORS");
            Console.WriteLine();
            parser.ShowErrors(2);
            Console.WriteLine();
            return;
        }

        // Display the provided arguments.
        Console.WriteLine("ARGUMENTS");
        Console.WriteLine();
        parser.ShowProvidedArguments(2);

        // Gather some essentials for generation.
        var filename = parser.GetOption<string>("file");
        var outputFolder = Path.GetDirectoryName(Path.GetFullPath(filename));
        var isDebug = parser.IsFlagProvided("debug");

        // Generate for all themes (name, theme details).
        Console.WriteLine();
        var generated = new Dictionary<string, string>();
        foreach (var sheetThemeName in Theme.GetStandardThemeNames())
        {
            var theme = Theme.GetStandardTheme(sheetThemeName);
            theme.TitleFont = Font.Parse(parser.GetOption<string>("titlefont")).Font;
            theme.SubtitleFont = Font.Parse(parser.GetOption<string>("subtitlefont")).Font;
            theme.AuthorFont = Font.Parse(parser.GetOption<string>("authorfont")).Font;
            theme.SeriesFont = Font.Parse(parser.GetOption<string>("seriesfont")).Font;

            var scaleAuthor = parser.IsFlagProvided("scaleauthor");
            var scaleSeries = parser.IsFlagProvided("scaleseries");
            var cover2 = new Cover2(900, 1350, 40, 40, 20, 40, 20, parser, theme, scaleAuthor, scaleSeries);
            generated.Add(sheetThemeName, cover2.GetSVG(isDebug));



            //var subtitle = parser.IsOptionProvided("subtitle")
            //    ? parser.GetOption<string>("subtitle") : "";
            //var cover = new Cover(
            //    900,
            //    1350,
            //    theme,
            //    parser.GetOption<string>("title").Replace("\\n", "\n").Replace("|", "\n"),
            //    subtitle.Replace("\\n", "\n").Replace("|", "\n"),
            //    parser.GetOption<string>("author").Replace("\\n", "\n").Replace("|", "\n"),
            //    parser.GetOption<string>("series").Replace("\\n", "\n").Replace("|", "\n"),
            //    parser.IsFlagProvided("scaleauthor"),
            //    parser.IsFlagProvided("scaleseries"));
            //generated.Add(sheetThemeName, cover.GetSVG(isDebug));
        }

        // Output the index sheet page with all the covers.
        IndexSheet.Write(filename, parser, generated);

        // Confirm.
        Console.WriteLine();
        Console.WriteLine("Done.");
        Console.WriteLine();
    }

    /// <summary>
    /// Checks the given option value is a font Name,Pixels
    /// value, returning a list of error messages if not.
    /// </summary>
    private static List<string> CheckFont(string key, object value)
    {
        var (errors, font) = Font.Parse($"{value}");
        return errors;
    }
}
