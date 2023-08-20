﻿using CreateCover.Models;

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
        Console.WriteLine("  " + Theme.GetStandardThemeNames());

        // Load and validate.
        config.AddCustomValidator("file", (conf, key, value) =>
        {
            if (Path.GetExtension((string)value).ToLowerInvariant() != ".svg")
                conf.AddError(key, "should be an SVG file");
        });
        config.AddCustomValidator("theme", (conf, key, value) =>
        {
            if (Theme.IsStandardTheme((string)value) == false)
                conf.AddError(key, "sets an unknown theme");
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
        var theme = Theme.GetStandardTheme(config.ProvidedStrings["theme"]);
        if (config.HasInt("titlefontsize")) theme.TitleFontSize = config.ProvidedInts["titlefontsize"];
        if (config.HasString("titlefont")) theme.TitleFonts = config.ProvidedStrings["titlefont"];
        if (config.HasString("authorfont")) theme.AuthorFont = config.ProvidedStrings["authorfont"];
        if (config.HasString("seriesfont")) theme.SeriesFont = config.ProvidedStrings["seriesfont"];

        var cover = new Cover(
            900,
            1350,
            theme,
            config.ProvidedStrings["title"].Replace("\\n", "\n").Replace("|", "\n"),
            config.ProvidedStrings["author"].Replace("\\n", "\n").Replace("|", "\n"),
            config.ProvidedStrings["series"].Replace("\\n", "\n").Replace("|", "\n"));

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
