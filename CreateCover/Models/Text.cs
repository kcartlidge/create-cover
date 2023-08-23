namespace CreateCover.Models
{
    /// <summary>Encapsulates a block of text.</summary>
    public class Text : ISVGElement
    {
        public Extent BoundingBox { get; private set; }
        public Anchor Anchor { get; private set; }

        public string FontNames { get; private set; }
        public int FontSize { get; private set; }
        public bool Scalable { get; private set; }
        public bool IsBold { get; private set; }
        public Color ForeColor { get; private set; }
        public string Content { get; private set; }
        public string Snippet => Content?.Length > 10
            ? ((Content ?? "").Substring(0, 10).TrimEnd() + "...")
            : Content ?? "";

        private int TextX = 0;
        private int TextY = 0;

        /// <summary>Start a new block of text.</summary>
        public Text(
            Extent boundingBox,
            Anchor anchor,
            string fontNames,
            int fontSize,
            bool isBold,
            Color foreColor,
            string content,
            bool scalable = false)
        {
            BoundingBox = boundingBox;
            Anchor = anchor;
            FontSize = fontSize;
            FontNames = fontNames.Trim();
            IsBold = isBold;
            ForeColor = foreColor;
            Scalable = scalable;
            Content = content.Trim();

            // Calculate the X position based on the anchor point.
            if (Anchor == Anchor.Left)
                (TextX, TextY) = (boundingBox.X1, boundingBox.Y2);
            if (Anchor == Anchor.Middle)
                (TextX, TextY) = (boundingBox.X1 + (boundingBox.Width / 2), boundingBox.Y2);
            if (Anchor == Anchor.Right)
                (TextX, TextY) = (boundingBox.X2, boundingBox.Y2);

            // Vertically centre within the bounding box.
            var offset = (boundingBox.Height / 2) - (fontSize / 2);
            TextY = boundingBox.Y2 - offset;
        }

        /// <summary>Get the SVG source.</summary>
        public string GetSVG(bool debugInfo)
        {
            if (debugInfo) Console.WriteLine(this);

            var anch = this.Anchor.ToString().ToLower();
            var bold = IsBold ? " font-weight=\"bold\"" : "";
            var txtLen = Scalable ? $" textLength=\"{BoundingBox.Width}\"" : "";
            var adjust = Scalable ? " lengthAdjust=\"spacingAndGlyphs\"" : "";

            return $"<text{bold}{adjust}{txtLen} x=\"{TextX}\" y=\"{TextY}\" font-family=\"{FontNames}\" font-size=\"{FontSize}px\" text-anchor=\"{anch}\" fill=\"{ForeColor}\">{Content}</text>";
        }

        public override string ToString()
        {
            return $"TEXT {BoundingBox}  POS {TextX},{TextY}  FS {FontSize}px ({ForeColor} {Anchor}) `{Snippet}`";
        }
    }
}
