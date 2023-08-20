using CreateCover.Models;

namespace CreateCover
{
    public class Cover
    {
        private readonly int width;
        private readonly int height;
        private readonly Theme theme;

        private readonly string titleText;
        private readonly string authorText;
        private readonly string seriesText;

        public Cover(
            int width,
            int height,
            Theme theme,
            string titleText,
            string authorText,
            string seriesText)
        {
            this.width = width;
            this.height = height;
            this.theme = theme;
            this.titleText = titleText;
            this.authorText = authorText;
            this.seriesText = seriesText;
        }

        public void Write(string svgFilename)
        {
            var titleTop = 0;
            var titlePad = 40;
            var authorTop = 1000;
            var authorPad = 10;
            var seriesTop = 1200;
            var seriesPad = 20;

            var cover = new SVG(width, height, theme.BackColor, theme.ForeColor, true);

            var stripe = new Rectangle(0, authorTop, cover.Width, seriesTop);
            stripe.Filled = true;
            stripe.Colors.BackColor = theme.AuthorBackColor;
            cover.Add(stripe);

            var lines = titleText.Split(
                "\n",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var titleFontSize = theme.TitleFontSize;
            var titleLinesTotalHeight = lines.Length * titleFontSize;
            var titleHeight = authorTop - titleTop - (titlePad * 2);
            var titleTopMargin = Math.Abs(titleHeight - titleLinesTotalHeight) / 2;
            var titleLineY = titleTop + titlePad + titleTopMargin;
            foreach (var line in lines)
            {
                var title = new TextBox(0, titleLineY, width, titleLineY + titleFontSize, theme.TitleFonts, Anchor.Middle, line);
                title.ForceFontSize(titleFontSize);
                title.ForeColor = theme.ForeColor;
                cover.Add(title);
                titleLineY += titleFontSize;
            }

            var author = new TextBox(0, authorTop + authorPad, width, seriesTop - authorPad, theme.AuthorFont, Anchor.Middle, authorText);
            author.ForeColor = theme.AuthorForeColor;
            cover.Add(author);

            var series = new TextBox(0, seriesTop + seriesPad, width, height - seriesPad, theme.SeriesFont, Anchor.Middle, seriesText);
            series.ForeColor = theme.ForeColor;
            cover.Add(series);

            cover.AddBorder(theme.BorderColor, 20);

            File.WriteAllText(svgFilename, cover.GetSVG());
        }
    }
}

