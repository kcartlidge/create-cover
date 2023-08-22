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
        Console.WriteLine("Generate a 900x1350 pixel SVG book cover");
        Console.WriteLine();

        // Define and show command arguments.
        Console.WriteLine("OPTIONS");
        Console.WriteLine();
        var parser = new Parser(args)
            .RequiresOption<string>("file", "where to write the output", "cover.svg")
            .RequiresOption<string>("title", "the book title (eg \"The | Fellowship | of the | Ring\")")
            .RequiresOption<string>("author", "the book author (eg \"JRR Tolkien\")")
            .RequiresOption<string>("series", "the book series (eg \"The Lord of the Rings 1\")")
            .SupportsOption<string>("theme", "the colour theme", "default")
            .SupportsOption<string>("titlefont", "title font names", "Impact,Tahoma,Arial")
            .SupportsOption<int>("titlefontsize", "size of title font in pixels", 180)
            .SupportsOption<string>("authorfont", "author font names", "Tahoma,Arial")
            .SupportsOption<int>("authorfontsize", "size of author font in pixels", 90)
            .SupportsOption<string>("seriesfont", "series font names", "Tahoma,Arial")
            .SupportsOption<int>("seriesfontsize", "size of series font in pixels", 90)
            .AddCustomOptionValidator("file", (string key, object filename) =>
            {
                var errs = new List<string>();
                var ext = Path.GetExtension($"{filename}").ToLowerInvariant();
                if (ext != ".svg") errs.Add($"Not an SVG filename: {filename}");
                return errs;
            })
            .AddCustomOptionValidator("theme", (string key, object themename) =>
            {
                var errs = new List<string>();
                if (Theme.IsStandardTheme((string)themename) == false)
                    errs.Add($"Unknown theme: {themename}");
                return errs;
            })
        ;
        parser.Help(2);
        Console.WriteLine("THEMES");
        Console.WriteLine();
        Console.WriteLine("  " + Theme.GetStandardThemeNames());
        Console.WriteLine();

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

        // Apply the theming.
        var theme = Theme.GetStandardTheme(parser.GetOption<string>("theme"));
        theme.TitleFonts = parser.GetOption<string>("titlefont");
        theme.AuthorFont = parser.GetOption<string>("authorfont");
        theme.SeriesFont = parser.GetOption<string>("seriesfont");
        theme.TitleFontSize = parser.GetOption<int>("titlefontsize");
        theme.AuthorFontSize = parser.GetOption<int>("authorfontsize");
        theme.SeriesFontSize = parser.GetOption<int>("seriesfontsize");

        // Generate.
        Console.WriteLine();
        Console.WriteLine("Generating SVG cover.");
        var cover = new Cover(
            900,
            1350,
            theme,
            parser.GetOption<string>("title").Replace("\\n", "\n").Replace("|", "\n"),
            parser.GetOption<string>("author").Replace("\\n", "\n").Replace("|", "\n"),
            parser.GetOption<string>("series").Replace("\\n", "\n").Replace("|", "\n"));

        // Output.
        var svgFilename = parser.GetOption<string>("file");
        cover.Write(svgFilename);

        // Confirm.
        Console.WriteLine($"Written SVG to {Path.GetFullPath(svgFilename)}");
        Console.WriteLine();
        Console.WriteLine("Open the SVG file in a Chromium browser (eg Brave),");
        Console.WriteLine("right-click on it and choose 'Inspect'. In the");
        Console.WriteLine("'Elements' right-click on the SVG node and choose");
        Console.WriteLine("'Capture node screenshot' to save it as a PNG.");
        Console.WriteLine();
    }
}
