namespace CreateCover
{
    public static class Extentions
    {
        /// <summary>
        /// Converts the text into lowercase with non-alpha-numerics replaced
        /// with single dashes.
        /// </summary>
        public static string Slugify(this string text)
        {
            var slug = "";
            foreach (var ch in text.Trim().ToLower())
            {
                if ("abcdefghijklmnopqrstuvwxyz0123456789".Contains(ch)) slug += ch;
                else slug += '-';
            }
            while (slug.Contains("--")) slug = slug.Replace("--", "-");
            return slug.Trim();
        }
    }
}
