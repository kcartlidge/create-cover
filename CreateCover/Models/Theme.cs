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
        public static List<string> GetStandardThemeNames()
        {
            return StandardThemes().Select(x => x.Key).ToList();
        }

        /// <summary>Get the collection of known standard schemes.</summary>
        public static string GetStandardThemeNamesCSV()
        {
            return string.Join(", ", GetStandardThemeNames());
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
                { "black", Black() },
                { "blue", Blue() },
                { "darkblue", DarkBlue() },
                { "green", Green() },
                { "darkgreen", DarkGreen() },
                { "red", Red() },
                { "darkred", DarkRed() },
                { "paleyellow", PaleYellow() },
                { "orange", Orange() },
                { "brown", Brown() },
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

        private static Theme Black()
        {
            var theme = Theme.Defaults();
            theme.BackColor = Color.Black;
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            return theme;
        }

        private static Theme Blue()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("246FB9");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return theme;
        }

        private static Theme Green()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("009933");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return theme;
        }

        private static Theme Red()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("E24040");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return theme;
        }

        private static Theme PaleYellow()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("DDDD88");
            theme.ForeColor = Color.Black;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            return theme;
        }

        private static Theme Orange()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("FFAA55");
            theme.ForeColor = Color.Black;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            return theme;
        }

        private static Theme Brown()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("945C22");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return theme;
        }

        private static Theme DarkBlue()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("1e5183");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            return theme;
        }

        private static Theme DarkGreen()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("5a7b4c");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            return theme;
        }

        private static Theme DarkRed()
        {
            var theme = Theme.Defaults();
            theme.BackColor = new Color("884444");
            theme.ForeColor = Color.White;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return theme;
        }
    }
}
