namespace CreateCover.Models
{
    /// <summary>Encapsulates a complete SVG cover.</summary>
    public class Cover
    {
        private readonly int width;
        private readonly int height;
        private readonly Theme theme;

        private readonly string titleText;
        private readonly string authorText;
        private readonly string seriesText;
        private readonly bool scaleAuthor;
        private readonly bool scaleSeries;

        /// <summary>Start an SVG cover.</summary>
        public Cover(
            int width,
            int height,
            Theme theme,
            string titleText,
            string authorText,
            string seriesText,
            bool scaleAuthor,
            bool scaleSeries)
        {
            this.width = width;
            this.height = height;
            this.theme = theme;
            this.titleText = titleText;
            this.authorText = authorText;
            this.seriesText = seriesText;
            this.scaleAuthor = scaleAuthor;
            this.scaleSeries = scaleSeries;
        }

        /// <summary>Write the SVG to the named file.</summary>
        public void Write(string filename, bool debugInfo = false)
        {
            // Predefined stuff.
            var padX = 75;
            var titlePadY = 10;
            var authorPadY = 10;
            var seriesPadY = 10;

            // Stripe vertical positioning.
            var slice = height / 8;
            var titleTop = 0;
            var titleBase = slice * 6;
            var authorTop = slice * 6;
            var authorBase = slice * 7;
            var seriesTop = slice * 7;
            var seriesBase = slice * 8;

            // Create the blocks with their text content.
            var titleBlock = new TextBox(
                0, titleTop, width - 1, titleBase, padX, titlePadY, titleText,
                theme.TitleFonts, theme.TitleFontSize, true, theme.BackColor, theme.ForeColor);
            var authorBlock = new TextBox(
                0, authorTop, width - 1, authorBase, padX, authorPadY, authorText,
                theme.AuthorFont, theme.AuthorFontSize, false, theme.AuthorBackColor,
                theme.AuthorForeColor, scaleAuthor);
            var seriesBlock = new TextBox(
                0, seriesTop, width - 1, seriesBase, padX, seriesPadY, seriesText,
                theme.SeriesFont, theme.SeriesFontSize, false, theme.BackColor,
                theme.ForeColor, scaleSeries);

            // Start the SVG and add the sections.
            var svg = new SVG(width, height, theme.BackColor, theme.ForeColor);
            svg.Add(new Rectangle(svg.BoundingBox, theme.BackColor, theme.BackColor));
            svg.Add(titleBlock);
            svg.Add(authorBlock);
            svg.Add(seriesBlock);

            // Add the border.
            svg.Add(new Rectangle(svg.BoundingBox, theme.BorderColor));

            if (debugInfo) Console.WriteLine();
            File.WriteAllText(filename, svg.GetSVG(debugInfo));
            if (debugInfo) Console.WriteLine();
        }
    }
}
