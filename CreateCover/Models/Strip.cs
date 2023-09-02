namespace CreateCover.Models
{
    /// <summary>Encapsulates a strip of content.</summary>
    public class Strip
    {
        public Extent Extent;
        public Extent TextArea;
        public Extent BackgroundArea;

        public Color BackColor;
        public Color ForeColor;
        public Font Font;
        public string Content;
        public bool Scalable;
        public bool Transparent;

        private readonly int PadX;
        private readonly int PadY;

        /// <summary>Start a new strip of content.</summary>
        public Strip(
            Extent extent,
            int padX,
            int padY,
            Color backColor,
            Color foreColor,
            Font font,
            string content,
            bool scalable,
            bool transparent)
        {
            Extent = extent;
            PadX = padX;
            PadY = padY;
            BackColor = backColor;
            ForeColor = foreColor;
            Font = font;
            Scalable = scalable;
            Transparent = transparent;
            Content = content.Replace("\\n", "\n").Replace("|", "\n");
            TextArea = new Extent(0, 0, 0, 0);
            BackgroundArea = new Extent(0, 0, 0, 0);

            UpdateDerivedExtents();
        }

        /// <summary>Recalculate the text area to account for padding.</summary>
        public void UpdateDerivedExtents()
        {
            BackgroundArea = new Extent(
                Extent.X1,
                Extent.Y1,
                Extent.X2,
                Extent.Y2);
            TextArea = new Extent(
                Extent.X1 + PadX,
                Extent.Y1 + PadY,
                Extent.X2 - PadX,
                Extent.Y2 - PadY);
        }
    }
}
