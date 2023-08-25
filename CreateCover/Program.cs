using System.Text;
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
        Console.WriteLine("Generate themed 900x1350 pixel book covers (SVG and PNG)");
        Console.WriteLine();

        // Define and show command arguments.
        Console.WriteLine("OPTIONS");
        Console.WriteLine();
        var parser = new Parser(args)
            .RequiresOption<string>("file", "where to write the output", "covers.html")
            .RequiresOption<string>("title", "the book title (eg \"The | Fellowship | of the | Ring\")")
            .RequiresOption<string>("author", "the book author (eg \"JRR Tolkien\")")
            .RequiresOption<string>("series", "the book series (eg \"The Lord of the Rings 1\")")
            .SupportsOption<string>("subtitle", "the book subtitle (eg titles in a box set)")
            .SupportsOption<string>("titlefont", "title font names", "Impact,Tahoma,Arial")
            .SupportsOption<int>("titlefontsize", "size of title font in pixels", 180)
            .SupportsOption<string>("subtitlefont", "subtitle font names", "Tahoma,Arial")
            .SupportsOption<int>("subtitlefontsize", "size of subtitle font in pixels", 75)
            .SupportsOption<string>("authorfont", "author font names", "Tahoma,Arial")
            .SupportsOption<int>("authorfontsize", "size of author font in pixels", 110)
            .SupportsOption<string>("seriesfont", "series font names", "Tahoma,Arial")
            .SupportsOption<int>("seriesfontsize", "size of series font in pixels", 100)
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

        // Determine what theme/themes need generating.
        var filename = parser.GetOption<string>("file");

        // Iterate through all requested themes (name, filename).
        var generated = new Dictionary<string, string>();
        var outputFolder = Path.GetDirectoryName(Path.GetFullPath(filename));
        var isDebug = parser.IsFlagProvided("debug");
        Console.WriteLine();

        // Generate.
        foreach (var sheetThemeName in Theme.GetStandardThemeNames())
        {
            var theme = Theme.GetStandardTheme(sheetThemeName);
            theme.TitleFonts = parser.GetOption<string>("titlefont");
            theme.SubtitleFonts = parser.GetOption<string>("subtitlefont");
            theme.AuthorFonts = parser.GetOption<string>("authorfont");
            theme.SeriesFonts = parser.GetOption<string>("seriesfont");
            theme.TitleFontSize = parser.GetOption<int>("titlefontsize");
            theme.SubtitleFontSize = parser.GetOption<int>("subtitlefontsize");
            theme.AuthorFontSize = parser.GetOption<int>("authorfontsize");
            theme.SeriesFontSize = parser.GetOption<int>("seriesfontsize");

            var subtitle = parser.IsOptionProvided("subtitle")
                ? parser.GetOption<string>("subtitle") : "";
            var cover = new Cover(
                900,
                1350,
                theme,
                parser.GetOption<string>("title").Replace("\\n", "\n").Replace("|", "\n"),
                subtitle.Replace("\\n", "\n").Replace("|", "\n"),
                parser.GetOption<string>("author").Replace("\\n", "\n").Replace("|", "\n"),
                parser.GetOption<string>("series").Replace("\\n", "\n").Replace("|", "\n"),
                parser.IsFlagProvided("scaleauthor"),
                parser.IsFlagProvided("scaleseries"));
            generated.Add(sheetThemeName, cover.GetSVG(isDebug));
        }

        // Output the index sheet page with all the covers.
        IndexSheet.Write(filename, parser, generated);

        // Confirm.
        Console.WriteLine();
        Console.WriteLine("Done.");
        Console.WriteLine();
    }
}
