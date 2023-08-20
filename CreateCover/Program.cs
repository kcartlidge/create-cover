using CreateCover.Models;

namespace CreateCover;

// See:
// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-width
// https://webdesign.tutsplus.com/how-to-hand-code-svg--cms-30368t
// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-width

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("CREATE COVER");
        Console.WriteLine();

        // Define and show command arguments.
        Config config = new Config(
            "file=\"cover.svg\" *",
            "title=\"The Hobbit\" *",
            "author=\"JRR Tolkien\" *",
            "series=\"Lord of the Rings 1\" *",
            "theme=\"default\" *",
            "titlefontsize=180",
            "titlefont=\"Impact,Verdana,Tahoma,Arial\"",
            "authorfont=\"Verdana,Tahoma,Arial\"",
            "seriesfont=\"Verdana,Tahoma,Arial\""
        );
        config.ShowInfo();
        Console.WriteLine();
        Console.WriteLine("THEMES:");
        Console.WriteLine("  default, dark, blue, green, orange, red, yellow");

        // Load and validate.
        config.AddCustomValidator("file", (conf, key, value) =>
        {
            if (Path.GetExtension((string)value).ToLowerInvariant() != ".svg")
                conf.AddError(key, "should be an SVG file");
        });
        config.SetFrom(args);
        if (config.HasError)
        {
            config.ShowAnyErrors();
            Console.WriteLine();
            return;
        }

        // Generate.
        config.ShowProvided();
        Console.WriteLine();
        Console.WriteLine("Generating SVG cover.");
        var theme = Theme.Green();
        var cover = new Cover(
            900,
            1350,
            theme,
            config.ProvidedStrings["title"].Replace("\\n", "\n"),
            config.ProvidedStrings["author"].Replace("\\n", "\n"),
            config.ProvidedStrings["series"].Replace("\\n", "\n"));

        var svgFilename = config.ProvidedStrings["file"];
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
