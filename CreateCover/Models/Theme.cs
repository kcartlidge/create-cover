namespace CreateCover.Models
{
    /// <summary>
    /// Encapsulates a theme.
    /// Also has static methods to return one or more standard schemes.
    /// </summary>
    public class Theme
    {
        public string Grouping = "";
        public string Colouring = "";
        public Color BackColor = Color.White;
        public Color ForeColor = Color.Black;
        public Color TitleForeColor = Color.Black;
        public Color AuthorBackColor = Color.White;
        public Color AuthorForeColor = Color.Black;
        public Color BorderColor = Color.Black;

        public Font TitleFont = new Font("", 0);
        public Font SubtitleFont = new Font("", 0);
        public Font AuthorFont = new Font("", 0, true);
        public Font SeriesFont = new Font("", 0);

        public string Name => $"{Grouping} {Colouring}";
        public bool IsTransparent => BackColor.IsTransparent;

        private static readonly Dictionary<string, Color> schemes = new()
            {
                {"White", Color.FromHTML("FFFFFF")},
                {"Black", Color.FromHTML("000000")},
                {"Dark Grey", Color.FromHTML("6E6E6E")},
                {"Red", Color.FromHTML("E24040")},
                {"Dark Red", Color.FromHTML("884444")},
                {"Cyan", Color.FromHTML("7FDBE6")},
                {"Blue", Color.FromHTML("246FB9")},
                {"Dark Blue", Color.FromHTML("1E5183")},
                {"Steel Blue", Color.FromHTML("476d76")},
                {"Green", Color.FromHTML("009933")},
                {"Dark Green", Color.FromHTML("5A7B4C")},
                {"Blue Wash", Color.FromHTML("76B5C5")},
                {"Red Wash", Color.FromHTML("E6A0A8")},
                {"Yellow", Color.FromHTML("F9EAA1")},
                {"Yellow-Green", Color.FromHTML("DDDD88")},
                {"Pale Yellow", Color.FromHTML("C5B35F")},
                {"Orange", Color.FromHTML("FFAA55")},
                {"Pale Orange", Color.FromHTML("F1CE9F")},
                {"Brown", Color.FromHTML("945C22")},
                {"Pale Violet", Color.FromHTML("C0ADDC")},
                {"Deep Purple", Color.FromHTML("5A4776")},
            };

        /// <summary>Start a new scheme.</summary>
        public static Theme Create(
            string grouping,
            string colouring,
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
                Grouping = grouping,
                Colouring = colouring,
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
            return StandardThemes().ContainsKey(themeName);
        }

        /// <summary>Get the collection of known standard scheme groupings.</summary>
        public static List<string> GetStandardThemeGroupings()
        {
            return StandardThemes().Select(x => x.Value.Grouping).ToList();
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
            return StandardThemes()[themeName];
        }

        private static Dictionary<string, Theme> StandardThemes()
        {
            var themes = new Dictionary<string, Theme>();
            var (w, b) = (Color.White, Color.Black);
            foreach (var (n, c) in schemes)
            {
                if (n == "White") continue;
                var t = NewTheme("Plain", n, w, b, c, w, b);
                if (c.IsTransparent) t.BackColor = c;
                themes.Add(t.Name, t);
            }
            foreach (var (n, c) in schemes)
            {
                if (n == "White") continue;
                var t = NewTheme("Plain Striped", n, w, b, b, c, w);
                if (c.IsLight()) t.AuthorForeColor = b;
                if (c.IsTransparent) t.BackColor = c;
                themes.Add(t.Name, t);
            }
            foreach (var (n, c) in schemes)
            {
                if (n == "White") continue;
                var t = NewTheme("Solid", n, c, b, b, c, b);
                if (c.IsDark)
                {
                    t.ForeColor = w;
                    t.TitleForeColor = w;
                    t.AuthorForeColor = w;
                }
                if (c.IsTransparent) t.BackColor = c;
                themes.Add(t.Name, t);
            }
            foreach (var (n, c) in schemes)
            {
                if (n == "White") continue;
                var t = NewTheme("Solid Striped", n, c, w, w, w, c);
                if (c.IsLight())
                {
                    t.ForeColor = b;
                    t.TitleForeColor = b;
                    t.AuthorBackColor = b;
                    t.AuthorForeColor = w;
                }
                if (c.IsTransparent) t.BackColor = c;
                themes.Add(t.Name, t);
            }
            return themes;
        }

        private static Theme NewTheme(
            string grouping,
            string colouring,
            Color backColor,
            Color foreColor,
            Color titleForeColor,
            Color authorBackColor,
            Color authorForeColor)
        {
            return Create(
                grouping,
                colouring,
                backColor, foreColor,
                titleForeColor,
                authorBackColor, authorForeColor,
                Color.Black,
                new Font("Impact", 180),
                new Font("Tahoma", 75),
                new Font("Tahoma", 110),
                new Font("Tahoma", 100)
                );
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
