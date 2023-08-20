namespace CreateCover.Models
{
    public class Line : IStepType
    {
        public int X1 = 1;
        public int Y1 = 1;
        public int X2 = 1;
        public int Y2 = 1;
        public int StrokeWidth = 1;

        public Color ForeColor = Color.Black;

        public Line(int x1, int y1, int x2, int y2, int strokeWidth)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            StrokeWidth = strokeWidth;
        }

        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public override string ToString()
        {
            return $"LINE {X1},{Y1} -> {X2},{Y2} ({ForeColor}, stroke {StrokeWidth})";
        }

        public string GetSVG()
        {
            return $"<line stroke=\"{ForeColor}\" stroke-width=\"{StrokeWidth}\" x1=\"{X1}\" y1=\"{Y1}\" x2=\"{X2}\" y2=\"{Y2}\"></line>";
        }
    }
}
