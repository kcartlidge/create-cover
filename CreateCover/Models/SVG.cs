using System.Text;

namespace CreateCover.Models
{
    public class SVG
    {
        public int Width = 900;
        public int Height = 1350;

        public int MinX => 0;
        public int MinY => 0;
        public int MaxX => Width - 1;
        public int MaxY => Height - 1;

        public readonly Colors Colors = new Colors();
        public readonly List<IStepType> Steps = new();

        public SVG(int width, int height, Color backColor, Color foreColor, bool filled)
        {
            Width = width;
            Height = height;
            Colors = new Colors(backColor, foreColor);

            var bg = GetBackgroundRectangle(Colors.BackColor, Colors.ForeColor);
            bg.Filled = filled;
            Steps.Add(bg);
        }

        public void AddBorder(Color borderColor, int strokeWidth)
        {
            var bg = GetBackgroundRectangle(borderColor, borderColor);
            bg.Filled = false;
            bg.StrokeWidth = strokeWidth;
            Steps.Add(bg);
        }

        public Rectangle GetBackgroundRectangle()
        {
            var area = new Rectangle(MinX, MinY, MaxX, MaxY);
            area.Colors = Colors;
            return area;
        }

        public Rectangle GetBackgroundRectangle(Color backColor, Color foreColor)
        {
            var area = new Rectangle(MinX, MinY, MaxX, MaxY);
            area.Colors = new Colors(backColor, foreColor);
            return area;
        }

        public void Add(IStepType step)
        {
            Steps.Add(step);
        }

        public string GetSVG()
        {
            var s = new StringBuilder();
            s.AppendLine($"<svg version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" width=\"{Width}\" height=\"{Height}\" viewBox=\"0 0 {Width} {Height}\">");
            foreach (var step in Steps) s.AppendLine(step.GetSVG());
            s.AppendLine("</svg>");
            return s.ToString();
        }

        public override string ToString()
        {
            return $"{Width},{Height} ({Colors})";
        }
    }
}
