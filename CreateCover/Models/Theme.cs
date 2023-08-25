﻿namespace CreateCover.Models
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

        public string TitleFonts = "";
        public string SubtitleFonts = "";
        public string AuthorFonts = "";
        public string SeriesFonts = "";

        public int TitleFontSize;
        public int SubtitleFontSize;
        public int AuthorFontSize;
        public int SeriesFontSize;

        /// <summary>Start a new scheme.</summary>
        public static Theme Create(
            Color backColor,
            Color foreColor,
            Color titleForeColor,
            Color authorBackColor,
            Color authorForeColor,
            Color borderColor,
            string titleFonts,
            string subtitleFonts,
            string authorFonts,
            string seriesFonts,
            int titleFontSize = 0,
            int subtitleFontSize = 0,
            int authorFontSize = 0,
            int seriesFontSize = 0)
        {
            return new Theme
            {
                BackColor = backColor,
                ForeColor = foreColor,
                TitleForeColor = titleForeColor,
                AuthorBackColor = authorBackColor,
                AuthorForeColor = authorForeColor,
                BorderColor = borderColor,
                TitleFonts = titleFonts,
                SubtitleFonts = subtitleFonts,
                AuthorFonts = authorFonts,
                SeriesFonts = seriesFonts,
                TitleFontSize = titleFontSize,
                SubtitleFontSize = subtitleFontSize,
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
            return new Dictionary<string, Theme>()
            {
                { Default().Name, Default().Settings },
                { DefaultBlue().Name, DefaultBlue().Settings },
                { DefaultDarkBlue().Name, DefaultDarkBlue().Settings },
                { DefaultGreen().Name, DefaultGreen().Settings },
                { DefaultDarkGreen().Name, DefaultDarkGreen().Settings },
                { DefaultRed().Name, DefaultRed().Settings },
                { DefaultDarkRed().Name, DefaultDarkRed().Settings },
                { DefaultBrown().Name, DefaultBrown().Settings },

                { OffYellow().Name, OffYellow().Settings },
                { Orange().Name, Orange().Settings },
                { Brown().Name, Brown().Settings },

                { Black().Name, Black().Settings },
                { Blue().Name, Blue().Settings },
                { DarkBlue().Name, DarkBlue().Settings },
                { Green().Name, Green().Settings },
                { DarkGreen().Name, DarkGreen().Settings },
                { Red().Name, Red().Settings },
                { DarkRed().Name, DarkRed().Settings },
            };
        }

        private static (string Name, Theme Settings) Default()
        {
            return ("default", Create(
                Color.White, Color.Black,
                Color.Black,
                Color.Black, Color.White,
                Color.Black,
                "Impact,Tahoma,Arial", "Tahoma,Arial",
                "Tahoma,Arial", "Tahoma,Arial"));
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
