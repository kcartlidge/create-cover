using System.Text;

namespace CreateCover.Models
{
    public class TextBox : ISVGElement
    {
        public SVG Surface { get; private set; }
        public Extent BoundingBox { get; private set; }
        public Extent TextArea { get; private set; }
        public string FontNames { get; private set; }
        public int FontSize { get; private set; }
        public bool IsBold { get; private set; }
        public int PadX { get; private set; }
        public int PadY { get; private set; }
        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }

        private Rectangle Background;
        private List<Text> Content;

        public TextBox(
            int x1, int y1, int x2, int y2, int padX, int padY,
            string text, string fontNames, int fontSize, bool isBold,
            Color backColor, Color foreColor)
        {
            BoundingBox = new Extent(x1, y1, x2, y2);
            FontNames = fontNames;
            FontSize = fontSize;
            IsBold = isBold;
            PadX = padX;
            PadY = padY;
            BackColor = backColor;
            ForeColor = foreColor;
            Background = new Rectangle(BoundingBox, BackColor, BackColor);
            Content = new List<Text>();

            var innerArea = new Extent(x1 + PadX, y1 + PadY, x2 - PadX, y2 - PadY);
            var lines = text.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var titleTextTop = y1 + (innerArea.Height - FontSize * lines.Length) / 2;
            for (var i = 0; i < lines.Length; i++)
            {
                var lineY = titleTextTop + i * FontSize;
                var lineArea = new Extent(innerArea.X1, lineY, innerArea.X2, lineY + FontSize);
                Content.Add(new Text(lineArea, Anchor.Middle, FontNames, FontSize, IsBold, ForeColor, lines[i]));
            }
        }

        public string GetSVG()
        {
            StringBuilder svg = new StringBuilder();
            svg.AppendLine(Background.GetSVG());
            foreach (var item in Content) svg.AppendLine(item.GetSVG());
            return svg.ToString();
        }
    }
}
