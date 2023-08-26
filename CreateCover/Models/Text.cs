using System.Text.Encodings.Web;

namespace CreateCover.Models
{
    /// <summary>Encapsulates a block of text.</summary>
    public class Text : ISVGElement
    {
        public Extent LineExtent { get; private set; }
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
            Extent lineExtent,
            Anchor anchor,
            string fontNames,
            int fontSize,
            bool isBold,
            Color foreColor,
            string content,
            bool scalable = false)
        {
            LineExtent = lineExtent;
            Anchor = anchor;
            FontSize = fontSize;
            FontNames = fontNames.Trim();
            IsBold = isBold;
            ForeColor = foreColor;
            Scalable = scalable;
            Content = HtmlEncoder.Default.Encode(content.Trim());

            // Calculate the X position based on the anchor point.
            if (Anchor == Anchor.Left)
                (TextX, TextY) = (lineExtent.X1, lineExtent.Y2);
            if (Anchor == Anchor.Middle)
                (TextX, TextY) = (lineExtent.X1 + (lineExtent.Width / 2), lineExtent.Y2);
            if (Anchor == Anchor.Right)
                (TextX, TextY) = (lineExtent.X2, lineExtent.Y2);

            // Vertically centre within the bounding box.
            TextY = LineExtent.MiddleY - 1;
        }

        /// <summary>Get the SVG source.</summary>
        public string GetSVG(bool debugInfo)
        {
            var anch = this.Anchor.ToString().ToLower();
            var bold = IsBold ? " font-weight=\"bold\"" : "";
            var txtLen = Scalable ? $" textLength=\"{LineExtent.Width}\"" : "";
            var baseLine = " alignment-baseline=\"central\"";
            var adjust = Scalable ? " lengthAdjust=\"spacingAndGlyphs\"" : "";
            var y = TextY;

            var svg = "";
            if (debugInfo)
            {
                var box = new Rectangle(LineExtent, ForeColor, null, 5);
                svg += box.GetSVG(false);
            }
            svg += $"<text{bold}{adjust}{baseLine}{txtLen} x=\"{TextX}\" y=\"{y}\" font-family=\"{FontNames}\" font-size=\"{FontSize}px\" text-anchor=\"{anch}\" fill=\"{ForeColor}\">{Content}</text>";
            return svg;
        }

        public override string ToString()
        {
            return $"TEXT {LineExtent}  POS {TextX},{TextY}  FS {FontSize}px ({ForeColor} {Anchor}) `{Snippet}`";
        }
    }
}
