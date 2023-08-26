namespace CreateCover.Models
{
    /// <summary>Encapsulates a complete SVG cover.</summary>
    public class Cover
    {
        private readonly int width;
        private readonly int height;
        private readonly Theme theme;

        private readonly string titleText;
        private readonly string subtitleText;
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
            string subtitleText,
            string authorText,
            string seriesText,
            bool scaleAuthor,
            bool scaleSeries)
        {
            this.width = width;
            this.height = height;
            this.theme = theme;
            this.titleText = titleText;
            this.subtitleText = subtitleText;
            this.authorText = authorText;
            this.seriesText = seriesText;
            this.scaleAuthor = scaleAuthor;
            this.scaleSeries = scaleSeries;
        }

        /// <summary>Get the SVG source.</summary>
        public string GetSVG(bool debugInfo = false)
        {
            // Predefined stuff.
            var hasSubtitle = (string.IsNullOrWhiteSpace(subtitleText) == false);
            var padX = 75;
            var titlePadY = 10;
            var subtitlePadY = 10;
            var authorPadY = 10;
            var seriesPadY = 10;

            // Stripe vertical positioning.
            var slice = height / 100;
            var titleTop = 0;
            var titleBase = slice * 78;
            var subtitleTop = titleBase;
            var subtitleBase = slice * 78;
            var authorTop = slice * 78;
            var authorBase = slice * 90;
            var seriesTop = slice * 95;
            var seriesBase = slice * 100;
            if (hasSubtitle)
            {
                titleBase = slice * 60;
                subtitleTop = slice * 50;
            }

            // Create the blocks with their text content.
            TextBox? subtitleBlock = null;
            var titleBlock = new TextBox(
                0, titleTop, width - 1, titleBase, padX, titlePadY, titleText,
                theme.TitleFont.Name, theme.TitleFont.Pixels, true, theme.BackColor, theme.TitleForeColor);
            if (string.IsNullOrWhiteSpace(subtitleText) == false)
            {
                subtitleBlock = new TextBox(
                    0, subtitleTop, width - 1, subtitleBase, padX, subtitlePadY, subtitleText,
                    theme.SubtitleFont.Name, theme.SubtitleFont.Pixels, false, theme.BackColor,
                    theme.ForeColor, false);
            }
            var authorBlock = new TextBox(
                0, authorTop, width - 1, authorBase, padX, authorPadY, authorText,
                theme.AuthorFont.Name, theme.AuthorFont.Pixels, false, theme.AuthorBackColor,
                theme.AuthorForeColor, scaleAuthor);
            var seriesBlock = new TextBox(
                0, seriesTop, width - 1, seriesBase, padX, seriesPadY, seriesText,
                theme.SeriesFont.Name, theme.SeriesFont.Pixels, false, theme.BackColor,
                theme.ForeColor, scaleSeries);

            // Start the SVG and add the sections.
            var svg = new SVG(width, height, theme.BackColor, theme.ForeColor);
            svg.Add(new Rectangle(svg.BoundingBox, theme.BackColor, theme.BackColor));
            svg.Add(titleBlock);
            if (subtitleBlock != null) svg.Add(subtitleBlock);
            svg.Add(authorBlock);
            svg.Add(seriesBlock);

            // Add the border.
            svg.Add(new Rectangle(svg.BoundingBox, theme.BorderColor, null, 30));

            return svg.GetSVG(debugInfo);
        }

        /// <summary>Write the SVG to the named file.</summary>
        public void Write(string filename, bool debugInfo = false)
        {
            File.WriteAllText(filename, GetSVG(debugInfo));
        }
    }
}
