namespace CreateCover.Models
{
    /// <summary>Encapsulates a complete rectangle (optionally filled).</summary>
    public class Rectangle : ISVGElement
    {
        public int X1 = 1;
        public int Y1 = 1;
        public int X2 = 1;
        public int Y2 = 1;
        public Color? BorderColor;
        public Color? FillColor;
        public int StrokeWidth = 1;

        public int Width => BoundingBox.Width;
        public int Height => BoundingBox.Height;

        public bool Filled => FillColor != null;
        public Extent BoundingBox => new Extent(X1, Y1, X2, Y2);

        /// <summary>
        /// Start a new rectangle.
        /// If you specify a fillColor it will be solid.
        /// </summary>
        public Rectangle(
            Extent boundingBox,
            Color? borderColor,
            Color? fillColor = null,
            int strokeWidth = 15)
        {
            X1 = boundingBox.X1;
            Y1 = boundingBox.Y1;
            X2 = boundingBox.X2;
            Y2 = boundingBox.Y2;
            BorderColor = borderColor;
            FillColor = fillColor;
            StrokeWidth = strokeWidth;
        }

        /// <summary>Get the SVG source.</summary>
        public string GetSVG(bool debugInfo)
        {
            if (debugInfo) Console.WriteLine(this);

            var fill = Filled ? $"{FillColor}" : "none";
            return $"<rect fill=\"{fill}\" stroke=\"{BorderColor}\" stroke-width=\"{StrokeWidth}\" x=\"{X1}\" y=\"{Y1}\" width=\"{Width}\" height=\"{Height}\"></rect>";
        }

        public override string ToString()
        {
            var fill = Filled ? $"{FillColor}" : "[none]";
            return $"RECT {BoundingBox}  BORDER {BorderColor} {StrokeWidth}px  FILL {fill}";
        }
    }
}
