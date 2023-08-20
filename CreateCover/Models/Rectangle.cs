namespace CreateCover.Models
{
    public class Rectangle : IStepType
    {
        public int X1 = 1;
        public int Y1 = 1;
        public int X2 = 1;
        public int Y2 = 1;
        public int StrokeWidth = 1;

        public int Width => X2 - X1 + 1;
        public int Height => Y2 - Y1 + 1;

        public bool Filled = false;
        public Colors Colors = new Colors();

        public List<Line> AsLines =>
            new()
            {
                new Line(X1, Y1, X2, Y1),  // top
                new Line(X1, Y2, X2, Y2),  // bottom
                new Line(X1, Y1, X1, Y2),  // left
                new Line(X2, Y1, X2, Y2),  // right
            };

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public override string ToString()
        {
            return $"RECT {X1},{Y1} -> {X2},{Y2} ({Colors})";
        }

        public string GetSVG()
        {
            var fill = Filled ? $"{Colors.BackColor}" : "none";
            return $"<rect fill=\"{fill}\" stroke=\"{Colors.ForeColor}\" stroke-width=\"{StrokeWidth}\" x=\"{X1}\" y=\"{Y1}\" width=\"{Width}\" height=\"{Height}\"></rect>";
        }
    }
}
