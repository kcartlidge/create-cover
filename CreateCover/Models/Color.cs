namespace CreateCover.Models
{
    /// <summary>
    /// Encapsulates the definition of a color.
    /// Includes static methods and constructors to enable
    /// speicfying colors using RGBA and HTML conventions.
    /// </summary>
    public class Color
    {
        private const string HexChars = "0123456789ABCDEF";

        public int Red = 0;
        public int Green = 0;
        public int Blue = 0;
        public int Alpha = 0;

        public static Color Black => new Color("000000");
        public static Color White => new Color("FFFFFF");
        public static Color Grey => new Color("BBBBBB");

        /// <summary>
        /// Create a new color from RGBA values.
        /// The A (alpha) is optional.
        /// </summary>
        public Color(int red, int green, int blue, int alpha = 255)
        {
            SetColor(red, green, blue, alpha);
        }

        /// <summary>Creates a new color from a 6 character HTML definition.</summary>
        public Color(string htmlRGBColor)
        {
            var wc = htmlRGBColor.Trim().ToUpperInvariant().TrimStart('#');
            if (wc.Length != 6) wc = "000000";
            var red = FromHex(wc.Substring(0, 2));
            var green = FromHex(wc.Substring(2, 2));
            var blue = FromHex(wc.Substring(4, 2));
            SetColor(red, green, blue, 255);
        }

        /// <summary>
        /// Create a new color from RGBA values.
        /// The A (alpha) is optional.
        /// </summary>
        public static Color FromRGBA(int red, int green, int blue, int alpha = 255)
        {
            return new Color(red, green, blue, alpha);
        }

        /// <summary>Creates a new color from a 6 character HTML definition.</summary>
        public static Color FromHTML(string htmlRGBColor)
        {
            return new Color(htmlRGBColor);
        }

        /// <summary>
        /// Returns the color as a HTML hex string.
        /// If the alpha is below 255 it will be included to
        /// form an 8 character string, otherwise it will be
        /// the standard 6 characters.
        /// There WILL NOT be a leading hash character.
        /// </summary>
        public string AsHex()
        {
            if (Alpha < 255)
                return $"{Red:X2}{Green:X2}{Blue:X2}{Alpha:X2}";
            else
                return $"{Red:X2}{Green:X2}{Blue:X2}";
        }

        public bool IsLight()
        {
            var average = (Red + Green + Blue) / 3;
            return average > 127;
        }

        public bool IsDark => IsLight() == false;

        /// <summary>
        /// Returns as per the AsHex() method, however in this
        /// case there WILL be a leading hash character.
        /// </summary>
        public override string ToString()
        {
            return "#" + AsHex();
        }

        private int FromHex(string twoHexDigits)
        {
            if (twoHexDigits.Length != 2) return 0;
            if (twoHexDigits.Any(x => HexChars.Contains(x) == false)) return 0;
            return (HexChars.IndexOf(twoHexDigits[0]) * 16)
                + HexChars.IndexOf(twoHexDigits[1]);
        }

        private void SetColor(int red, int green, int blue, int alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
    }
}
