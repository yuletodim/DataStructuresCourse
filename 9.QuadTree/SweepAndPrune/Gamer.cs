namespace SweepAndPrune
{
    using System;

    public class Gamer
    {
        private const int width = 10;
        private const int height = 10;

        private string name;

        public Gamer(string name, int x, int y)
        {
            this.Name = name;
            this.X1 = x;
            this.Y1 = y;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        public int X1 { get; set; }
        
        public int Y1 { get; set; }

        public int X2 { get { return X1 + width; } }

        public int Y2 { get { return Y1 + height; } }

        internal bool Intersects(Gamer other)
        {
            return this.X1 <= other.X2 &&
                other.X1 <= this.X2 &&
                this.Y1 <= other.Y2 &&
                other.Y1 <= this.Y2;
        }
    }
}
