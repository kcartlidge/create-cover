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
        public Color TitleForeColor = Color.Black;
        public Color AuthorBackColor = Color.White;
        public Color AuthorForeColor = Color.Black;
        public Color BorderColor = Color.Black;

        public Font TitleFont = new Font("", 0);
        public Font SubtitleFont = new Font("", 0);
        public Font AuthorFont = new Font("", 0);
        public Font SeriesFont = new Font("", 0);

        /// <summary>Start a new scheme.</summary>
        public static Theme Create(
            Color backColor,
            Color foreColor,
            Color titleForeColor,
            Color authorBackColor,
            Color authorForeColor,
            Color borderColor,
            Font titleFont,
            Font subtitleFont,
            Font authorFont,
            Font seriesFont)
        {
            return new Theme
            {
                BackColor = backColor,
                ForeColor = foreColor,
                TitleForeColor = titleForeColor,
                AuthorBackColor = authorBackColor,
                AuthorForeColor = authorForeColor,
                BorderColor = borderColor,
                TitleFont = titleFont,
                SubtitleFont = subtitleFont,
                AuthorFont = authorFont,
                SeriesFont = seriesFont,
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
            var themes = new Dictionary<string, Theme>();
            foreach (var theme in new[] {
                Default,
                DefaultBlue, DefaultDarkBlue,
                DefaultGreen, DefaultDarkGreen,
                DefaultRed, DefaultDarkRed,
                DefaultBrown,
                OffYellow, Orange, Brown, Black,
                Blue, DarkBlue,
                Green, DarkGreen,
                Red, DarkRed,
            })
                themes.Add(theme().Name, theme().Settings);
            return themes;
        }

        private static (string Name, Theme Settings) Default()
        {
            return ("default", Create(
                Color.White, Color.Black,
                Color.Black,
                Color.Black, Color.White,
                Color.Black,
                new Font("Impact", 180),
                new Font("Tahoma", 75),
                new Font("Tahoma", 110),
                new Font("Tahoma", 100)
                ));
        }

        private static (string Name, Theme Settings) Black()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.Black;
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            return ("black", theme);
        }

        private static (string Name, Theme Settings) DefaultBlue()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("246FB9");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-blue", theme);
        }

        private static (string Name, Theme Settings) DefaultDarkBlue()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("1e5183");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-dark-blue", theme);
        }

        private static (string Name, Theme Settings) Blue()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("246FB9");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("blue", theme);
        }

        private static (string Name, Theme Settings) DarkBlue()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("1e5183");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            return ("dark-blue", theme);
        }

        private static (string Name, Theme Settings) DefaultGreen()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("009933");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-green", theme);
        }

        private static (string Name, Theme Settings) DefaultDarkGreen()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("5a7b4c");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-dark-green", theme);
        }

        private static (string Name, Theme Settings) Green()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("009933");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("green", theme);
        }

        private static (string Name, Theme Settings) DarkGreen()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("5a7b4c");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = Color.White;
            theme.AuthorForeColor = Color.Black;
            return ("dark-green", theme);
        }

        private static (string Name, Theme Settings) DefaultRed()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("E24040");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-red", theme);
        }

        private static (string Name, Theme Settings) DefaultDarkRed()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("884444");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-dark-red", theme);
        }

        private static (string Name, Theme Settings) Red()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("E24040");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("red", theme);
        }

        private static (string Name, Theme Settings) DarkRed()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("884444");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("dark-red", theme);
        }

        private static (string Name, Theme Settings) OffYellow()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("DDDD88");
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            return ("off-yellow", theme);
        }

        private static (string Name, Theme Settings) Orange()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("FFAA55");
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = Color.White;
            return ("orange", theme);
        }

        private static (string Name, Theme Settings) Brown()
        {
            var (_, theme) = Default();
            theme.BackColor = new Color("945C22");
            theme.ForeColor = Color.White;
            theme.TitleForeColor = theme.ForeColor;
            theme.AuthorBackColor = theme.ForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("brown", theme);
        }

        private static (string Name, Theme Settings) DefaultBrown()
        {
            var (_, theme) = Default();
            theme.BackColor = Color.White;
            theme.ForeColor = Color.Black;
            theme.TitleForeColor = new Color("945C22");
            theme.AuthorBackColor = theme.TitleForeColor;
            theme.AuthorForeColor = theme.BackColor;
            return ("default-brown", theme);
        }
    }
}
