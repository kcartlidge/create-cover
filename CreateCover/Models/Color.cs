namespace CreateCover.Models
{
    public class Color
    {
        private const string HexChars = "0123456789ABCDEF";

        public int Red = 0;
        public int Green = 0;
        public int Blue = 0;
        public int Alpha = 0;

        public static Color Black => new Color("000000");
        public static Color White => new Color("FFFFFF");

        public Color(int red, int green, int blue, int alpha)
        {
            SetColor(red, green, blue, alpha);
        }

        public Color(int red, int green, int blue)
        {
            SetColor(red, green, blue, 255);
        }

        public Color(string htmlRGBColor)
        {
            var wc = htmlRGBColor.Trim().ToUpperInvariant().TrimStart('#');
            if (wc.Length != 6) wc = "000000";
            var red = FromHex(wc.Substring(0, 2));
            var green = FromHex(wc.Substring(2, 2));
            var blue = FromHex(wc.Substring(4, 2));
            SetColor(red, green, blue, 255);
        }

        public string AsHex()
        {
            if (Alpha < 255)
                return $"{Red:X2}{Green:X2}{Blue:X2}{Alpha:X2}";
            else
                return $"{Red:X2}{Green:X2}{Blue:X2}";
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

        public override string ToString()
        {
            return "#" + AsHex();
        }
    }
}
