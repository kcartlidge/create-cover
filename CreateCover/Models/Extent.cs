namespace CreateCover.Models
{
    /// <summary>Encapsulates the definition of an area.</summary>
    public class Extent
    {
        public int X1 = 1;
        public int Y1 = 1;
        public int X2 = 1;
        public int Y2 = 1;

        public int Width => X2 - X1 + 1;
        public int Height => Y2 - Y1 + 1;
        public int MiddleY => Y1 + (Height / 2);

        /// <summary>Start a new extent for the given area.</summary>
        public Extent(int X1, int Y1, int X2, int Y2)
        {
            this.X1 = Math.Max(0, Math.Abs(X1));
            this.Y1 = Math.Max(0, Math.Abs(Y1));
            this.X2 = Math.Max(X1, Math.Abs(X2));
            this.Y2 = Math.Max(Y1, Math.Abs(Y2));
        }

        public override string ToString()
        {
            return $"{X1},{Y1} -> {X2},{Y2}  {Width}x{Height}";
        }
    }
}
