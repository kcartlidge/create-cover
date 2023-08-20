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
        Console.WriteLine("Generating SVG cover.");
        var theme = Theme.Green();
        var cover = new Cover(
            900,
            1350,
            theme,
            "Down\nAmong\nthe\nDead Men",
            "Simon R Green",
            "Forest Kingdom 3");

        var svgFilename = "cover.svg";
        cover.Write(svgFilename);

        Console.WriteLine($"Written SVG to {Path.GetFullPath(svgFilename)}");
        Console.WriteLine();
        Console.WriteLine("Open the SVG file in a Chromium browser (eg Brave),");
        Console.WriteLine("right-click on it and choose 'Inspect'. In the");
        Console.WriteLine("'Elements' right-click on the SVG node and choose");
        Console.WriteLine("'Capture node screenshot' to save it as a PNG.");
        Console.WriteLine();
    }
}
