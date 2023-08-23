using System.Text;

namespace CreateCover.Models
{
    /// <summary>Encapsulates an SVG image definition.</summary>
    public class SVG
    {
        public int Width;
        public int Height;

        public int MinX => 0;
        public int MinY => 0;
        public int MaxX => Width - 1;
        public int MaxY => Height - 1;

        public readonly Color BackColor;
        public readonly Color ForeColor;
        public readonly List<ISVGElement> Elements = new();

        public Extent BoundingBox => new Extent(MinX, MinY, MaxX, MaxY);

        /// <summary>Start a new SVG image.</summary>
        public SVG(int width, int height, Color backColor, Color foreColor)
        {
            Width = width;
            Height = height;
            BackColor = backColor;
            ForeColor = foreColor;
        }

        /// <summary>Add (and return) the provided element.</summary>
        public ISVGElement Add(ISVGElement element)
        {
            Elements.Add(element);
            return element;
        }

        /// <summary>Get the SVG source.</summary>
        public string GetSVG(bool debugInfo)
        {
            if (debugInfo) Console.WriteLine(this);

            var s = new StringBuilder();
            s.AppendLine($"<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" width=\"{Width}\" height=\"{Height}\" viewBox=\"0 0 {Width} {Height}\">");
            foreach (var step in Elements) s.Append(step.GetSVG(debugInfo));
            s.AppendLine();
            s.Append("</svg>");
            return s.ToString();
        }

        public override string ToString()
        {
            return $"SVG {BoundingBox}  {ForeColor} ON {BackColor}  ELEMENTS {Elements.Count}";
        }
    }
}
