using ArgsParser;

namespace CreateCover.Models
{
    /// <summary>
    /// Encapsulates all element positions and sizes.
    /// Includes the calculations necessary to place things.
    /// </summary>
	public class Cover
    {
        public int Width;
        public int Height;

        public int NextY;
        public int CoverPadX;
        public int CoverPadY;
        public int StripPadX;
        public int StripPadY;
        public Parser Parser;
        public Theme Theme;
        public bool ScaleAuthor;
        public bool ScaleSeries;

        private readonly int BorderStroke;
        private readonly string TitleText;
        private readonly string SubtitleText;
        private readonly string AuthorText;
        private readonly string SeriesText;

        private Strip TitleStrip;
        private Strip? SubtitleStrip = null;
        private Strip AuthorStrip;
        private Strip SeriesStrip;

        public Cover(
            int width,
            int height,
            int coverPadX,
            int coverPadY,
            int borderStroke,
            int stripPadX,
            int stripPadY,
            Parser parser,
            Theme theme,
            bool scaleAuthor,
            bool scaleSeries)
        {
            Width = width;
            Height = height;
            CoverPadX = coverPadX;
            CoverPadY = coverPadY;
            BorderStroke = borderStroke;
            StripPadX = stripPadX;
            StripPadY = stripPadY;
            Parser = parser;
            Theme = theme;
            NextY = (borderStroke / 2) + coverPadY;
            ScaleAuthor = scaleAuthor;
            ScaleSeries = scaleSeries;

            var hasSubtitle = parser.IsOptionProvided("subtitle");
            var subtitle = hasSubtitle ? parser.GetOption<string>("subtitle") : "";
            TitleText = parser.GetOption<string>("title");
            SubtitleText = subtitle.Replace("\\n", "\n");
            AuthorText = parser.GetOption<string>("author");
            SeriesText = parser.GetOption<string>("series");

            // If debug, adjust the colours for guaranteed visibility.
            if (parser.IsFlagProvided("debug"))
                Theme.AuthorForeColor = Theme.ForeColor;

            // Derive the strips running naively down the page.
            TitleStrip = new Strip(
                GetExtent(theme.TitleFont.Pixels, TitleText),
                    coverPadX + stripPadX, stripPadY,
                    theme.BackColor, theme.TitleForeColor,
                    theme.TitleFont, TitleText, false);
            if (Parser.IsOptionProvided("subtitle"))
            {
                NextY += theme.SubtitleFont.Pixels / 4;
                SubtitleStrip = new Strip(
                    GetExtent(theme.SubtitleFont.Pixels, SubtitleText),
                    coverPadX + stripPadX, stripPadY,
                    theme.BackColor, theme.ForeColor,
                    theme.SubtitleFont, SubtitleText, false);
            }
            AuthorStrip = new Strip(
                GetExtent(theme.AuthorFont.Pixels, AuthorText),
                    coverPadX + stripPadX, stripPadY,
                    theme.AuthorBackColor, theme.AuthorForeColor,
                    theme.AuthorFont, AuthorText, ScaleAuthor);
            SeriesStrip = new Strip(
                GetExtent(theme.SeriesFont.Pixels, SeriesText),
                    coverPadX + stripPadX, stripPadY,
                    theme.BackColor, theme.ForeColor,
                    theme.SeriesFont, SeriesText, ScaleSeries);

            // Adjust the author and series to be bottom-aligned.
            SeriesStrip = MoveStrip(SeriesStrip, null, Height - (borderStroke / 2) - coverPadY);
            AuthorStrip = MoveStrip(AuthorStrip, null, SeriesStrip.Extent.Y1 - 1);

            // Adjust the title and subtitle to be middle-aligned in the space above.
            var availableHeight = AuthorStrip.Extent.Y1 - CoverPadY;
            var requiredHeight = TitleStrip.Extent.Height;
            if (hasSubtitle) requiredHeight = SubtitleStrip.Extent.Y2 - TitleStrip.Extent.Y1;
            var halfHeightDifference = (availableHeight - requiredHeight) / 2;
            TitleStrip = MoveStrip(TitleStrip, TitleStrip.Extent.Y1 + halfHeightDifference, null);
            if (hasSubtitle)
                SubtitleStrip = MoveStrip(SubtitleStrip, TitleStrip.Extent.Y2 + 1, null);
        }

        /// <summary>Get the SVG source.</summary>
        public string GetSVG(bool debugInfo = false)
        {
            // Start the SVG and add the sections.
            var svg = new SVG(Width, Height, Theme.BackColor, Theme.ForeColor);
            svg.Add(new Rectangle(svg.BoundingBox, Theme.BackColor, Theme.BackColor));

            // Add the strips backgrounds.
            var strips = new[] { TitleStrip, SubtitleStrip, AuthorStrip, SeriesStrip };
            foreach (var strip in strips)
            {
                if (strip == null) continue;
                if (debugInfo)
                {
                    svg.Add(new Rectangle(strip.TextArea, Color.Black, null, 4));
                }
                else
                {
                    svg.Add(new Rectangle(strip.BackgroundArea, null, strip.BackColor));
                }
                var y = strip.TextArea.Y1;
                var lineHeight = strip.Font.Pixels;
                foreach (var line in strip.Content.Split("\n", StringSplitOptions.TrimEntries))
                {
                    var lineExtent = new Extent(strip.TextArea.X1, y, strip.TextArea.X2, y + lineHeight);
                    var text = new Text(lineExtent, Anchor.Middle,
                        strip.Font.Name, strip.Font.Pixels,
                        strip.Font.Bold, strip.ForeColor, line, strip.Scalable);
                    svg.Add(text);
                    y += lineHeight;
                }
            }

            // Add the border.
            svg.Add(new Rectangle(svg.BoundingBox, Theme.BorderColor, null, BorderStroke));

            return svg.GetSVG(debugInfo);
        }

        /// <summary>
        /// Relocates the given strip vertically, moving either
        /// the top or the bottom of the extent to the position
        /// specified by whichever of y1 or y2 is >= 0.
        /// </summary>
        private Strip MoveStrip(Strip strip, int? y1, int? y2)
        {
            if (y1.HasValue)
            {
                var dy = y1.Value - strip.Extent.Y1;
                strip.Extent.Y1 = y1.Value;
                strip.Extent.Y2 = strip.Extent.Y2 + dy;
            }
            else if (y2.HasValue)
            {
                var dy = y2.Value - strip.Extent.Y2;
                strip.Extent.Y1 = strip.Extent.Y1 + dy;
                strip.Extent.Y2 = y2.Value;
            }
            strip.UpdateDerivedExtents();
            return strip;
        }

        /// <summary>
        /// Returns the extent size needed for the content given.
        /// Supports multi-line text using pipes or line feeds.
        /// </summary>
        private Extent GetExtent(int fontPixels, string content)
        {
            // Derive values based on the strip being calculated.
            var lineCount = content
                .Replace("\n", "|")
                .Split('|', StringSplitOptions.TrimEntries)
                .Length;

            // Sizes are straightforward.
            var x1 = 0; // CoverPadX;
            var x2 = Width; // Width - CoverPadX;
            var y1 = NextY;
            var y2 = NextY + (StripPadY * 2) + (fontPixels * lineCount) - 1;
            var extent = new Extent(x1, y1, x2, y2);
            NextY = y2 + 1;
            return extent;
        }
    }
}
