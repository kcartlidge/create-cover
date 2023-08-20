namespace CreateCover.Models
{
    public class TextBox : IStepType
    {
        public int X1 = 1;
        public int Y1 = 1;
        public int X2 = 1;
        public int Y2 = 1;
        public string FontFamily = "Verdana";
        public Anchor Anchor = Anchor.None;
        public Color ForeColor = Color.Black;
        public string Content = "";
        public bool IsBold = false;

        private int? FontSize = null;

        public TextBox(
            int x1,
            int y1,
            int x2,
            int y2,
            string fontName,
            Anchor anchor,
            string content)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            FontFamily = fontName;
            Anchor = anchor;
            Content = content.Trim();
        }

        public void ForceFontSize(int fontSizeInPixels)
        {
            FontSize = fontSizeInPixels;
        }

        public override string ToString()
        {
            return $"TEXT {X1},{Y1} -> {X2},{Y2} {FontFamily} ({ForeColor}, anchor {Anchor})";
        }

        public string GetSVG()
        {
            var anch = this.Anchor.ToString().ToLower();

            // Max width is 85% of the text box width.
            var textLength = ((X2 - X1) * 85) / 100;

            // Font size is 85% of the text box height.
            var fontSize = ((Y2 - Y1) * 85) / 100;

            // Unless overridden!
            fontSize = FontSize ?? fontSize;

            // Font X is dependent upon the anchor position.
            var x = X1;
            if (this.Anchor == Anchor.Middle) x = (X2 - X1) / 2;
            if (this.Anchor == Anchor.Right) x = X2;

            // Text Y is where the font baseline goes.
            // This will be the bottom minus 20% of the height.
            var y = Y2 - ((Y2 - Y1) * 20) / 100;

            var bold = IsBold ? " font-weight=\"bold\"" : "";
            var adjust = FontSize == null ? " lengthAdjust=\"spacingAndGlyphs\"" : "";
            var txtLen = FontSize == null ? $" textLength=\"{textLength}\"" : "";
            return $"<text{bold}{adjust}{txtLen} x=\"{x}\" y=\"{y}\" font-family=\"{FontFamily}\" font-size=\"{fontSize}px\" text-anchor=\"{anch}\" fill=\"{ForeColor}\">{Content}</text>";
        }
    }
}
