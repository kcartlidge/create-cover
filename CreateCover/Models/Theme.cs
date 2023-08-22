namespace CreateCover.Models
{
    /// <summary>
    /// Encapsulates a theme.
    /// Also has static methods to return one or more standard schemes.
    /// </summary>
    public class Theme
    {
        public Color BackColor = Color.White;
        public Color ForeColor = Color.Black;
        public Color AuthorBackColor = Color.White;
        public Color AuthorForeColor = Color.Black;
        public Color BorderColor = Color.Black;

        public string TitleFonts = "";
        public string AuthorFont = "";
        public string SeriesFont = "";

        public int TitleFontSize;
        public int AuthorFontSize;
        public int SeriesFontSize;

        /// <summary>Start a new scheme.</summary>
        public static Theme Create(
            Color backColor,
            Color foreColor,
            Color authorBackColor,
            Color authorForeColor,
            Color borderColor,
            string titleFonts,
            string authorFont,
            string seriesFont,
            int titleFontSize,
            int authorFontSize,
            int seriesFontSize)
        {
            return new Theme
            {
                BackColor = backColor,
                ForeColor = foreColor,
                AuthorBackColor = authorBackColor,
                AuthorForeColor = authorForeColor,
                BorderColor = borderColor,
                TitleFonts = titleFonts,
                AuthorFont = authorFont,
                SeriesFont = seriesFont,
                TitleFontSize = titleFontSize,
                AuthorFontSize = authorFontSize,
                SeriesFontSize = seriesFontSize,
            };
        }

        /// <summary>Is this a known standard scheme?</summary>
        public static bool IsStandardTheme(string themeName)
        {
            return StandardThemes().ContainsKey(themeName.ToLowerInvariant());
        }

        /// <summary>Get the collection of known standard schemes.</summary>
        public static string GetStandardThemeNames()
        {
            return string.Join(", ", StandardThemes().Select(x => x.Key));
        }

        /// <summary>Return the named standard scheme.</summary>
        public static Theme GetStandardTheme(string themeName)
        {
            return StandardThemes()[themeName.ToLowerInvariant()];
        }

        private static Dictionary<string, Theme> StandardThemes()
        {
            var themes = new Dictionary<string, Theme>
            {
                { "default", Defaults() },
                { "dark", Dark() },
                { "blue", Blue() },
                { "green", Green() },
                { "red", Red() },
                { "yellow", Yellow() },
                { "orange", Orange() },
            };
            return themes;
        }

        private static Theme Defaults()
        {
            return Theme.Create(
                Color.White,
                Color.Black,
                Color.Black,
                Color.White,
                Color.Black,
                "Impact,Tahoma,Arial",
                "Tahoma,Arial",
                "Tahoma,Arial",
                0,
                0,
                0);
        }

        private static Theme Dark()
        {
            var theme = Theme.Defaults();
            theme.BackColor = Color.Black;
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            theme.BorderColor = Color.Black;
            return theme;
        }

        private static Theme Blue()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("004488");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            theme.BorderColor = theme.BackColor;
            return theme;
        }

        private static Theme Green()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("008844");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            theme.BorderColor = theme.BackColor;
            return theme;
        }

        private static Theme Red()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("884444");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            theme.BorderColor = theme.BackColor;
            return theme;
        }

        private static Theme Yellow()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("DDDD88");
            theme.ForeColor = Color.Black;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            theme.BorderColor = Color.Black;
            return theme;
        }

        private static Theme Orange()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("FFAA55");
            theme.ForeColor = Color.Black;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            theme.BorderColor = Color.Black;
            return theme;
        }
    }
}
