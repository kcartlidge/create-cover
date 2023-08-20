namespace CreateCover.Models
{
    public class Colors
	{
        public Color BackColor = Color.White;
        public Color ForeColor = Color.Black;

        public Colors(Color backColor, Color foreColor)
        {
            BackColor = backColor;
            ForeColor = foreColor;
        }

        public Colors()
        {
        }

        public override string ToString()
        {
            return $"{ForeColor} on {BackColor}";
        }
    }
}
