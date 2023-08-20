namespace CreateCover.Models
{
    public class Theme
    {
        public Color BackColor = Color.White;
        public Color ForeColor = Color.Black;
        public Color AuthorBackColor = Color.White;
        public Color AuthorForeColor = Color.Black;
        public Color BorderColor = Color.Black;

        public string TitleFonts = "";
        public int TitleFontSize;

        public string AuthorFont = "";
        public string SeriesFont = "";

        public static Theme Create(
            Color backColor,
            Color foreColor,
            Color authorBackColor,
            Color authorForeColor,
            Color borderColor,
            string titleFonts,
            int titleFontSize,
            string authorFont,
            string seriesFont)
        {
            return new Theme
            {
                BackColor = backColor,
                ForeColor = foreColor,
                AuthorBackColor = authorBackColor,
                AuthorForeColor = authorForeColor,
                BorderColor = borderColor,
                TitleFonts = titleFonts,
                TitleFontSize = titleFontSize,
                AuthorFont = authorFont,
                SeriesFont = seriesFont,
            };
        }

        public static Theme Monochrome()
        {
            return Theme.Create(
                Color.White,
                Color.Black,
                Color.Black,
                Color.White,
                Color.Black,
                "Impact,Tahoma,Arial",
                180,
                "Tahoma,Arial",
                "Tahoma,Arial");
        }

        public static Theme Dark()
        {
            var theme = Theme.Monochrome();
            theme.BackColor = Color.Black;
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            theme.BorderColor = Color.Black;
            return theme;
        }

        public static Theme Blue()
        {
            var theme = Theme.Monochrome();
            theme.BackColor = new Color("004488");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            theme.BorderColor = theme.BackColor;
            return theme;
        }

        public static Theme Green()
        {
            var theme = Theme.Monochrome();
            theme.BackColor = new Color("008844");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            theme.BorderColor = theme.BackColor;
            return theme;
        }

        public static Theme Red()
        {
            var theme = Theme.Monochrome();
            theme.BackColor = new Color("884444");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            theme.BorderColor = theme.BackColor;
            return theme;
        }

        public static Theme Yellow()
        {
            var theme = Theme.Monochrome();
            theme.BackColor = new Color("DDDD88");
            theme.ForeColor = Color.Black;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            theme.BorderColor = Color.Black;
            return theme;
        }

        public static Theme Orange()
        {
            var theme = Theme.Monochrome();
            theme.BackColor = new Color("FFAA55");
            theme.ForeColor = Color.Black;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            theme.BorderColor = Color.Black;
            return theme;
        }
    }
}
