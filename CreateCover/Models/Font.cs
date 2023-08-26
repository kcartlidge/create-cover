namespace CreateCover.Models
{
    /// <summary>
    /// Encapsulates a font.
    /// Also has a static method to create a font from a `name,pixel` string.
    /// </summary>
    public class Font
	{
		public string Name = "";
		public int Pixels = 0;

        /// <summary>Start a new font.</summary>
		public Font(string name, int pixels)
		{
            Name = string.IsNullOrWhiteSpace(name) ? "sans-serif" : name;
            Pixels = pixels < 1 ? 1 : pixels;
        }

        /// <summary>
        /// Start a new font from a `name,pixel` string.
        /// Returns a list of error strings and a font if one
        /// could be created from the provided text.
        /// </summary>
        public static (List<string> Errors, Font Font) Parse(string text)
        {
            // Assume nothing.
            var errors = new List<string>();
            var font = new Font("", 0);
            int pixels = 0;

            // Separate out the string portions.
            var bits = text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            font.Name = bits[0];

            // Sanity checks.
            if (bits.Length > 2)
            {
                errors.Add($"Expected only 2 values (font,pixels): {text}");
                return (errors, font);
            }
            if (string.IsNullOrWhiteSpace(font.Name))
            {
                errors.Add($"Cannot extract font name: {text}");
                return (errors, font);
            }
            if (int.TryParse(bits[0], out var dummy))
            {
                errors.Add($"Expected font,pixels: {text}");
                return (errors, new Font("", 0));
            }
            if (bits.Length == 2 && int.TryParse(bits[1], out pixels) == false)
            {
                errors.Add($"Expected integer pixels value: {text}");
                return (errors, font);
            }
            if (pixels < 1)
            {
                errors.Add($"Font size must be at least 1 pixel: {text}");
                return (errors, font);
            }

            // Update the font and we're done.
            font.Pixels = pixels;
            return (errors, font);
        }

        /// <summary>
        /// Return in the same format use by the Font.Parse static method.
        /// </summary>
        public override string ToString()
        {
            return $"{Name},{Pixels}";
        }
    }
}

