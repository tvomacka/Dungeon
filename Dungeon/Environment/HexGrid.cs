using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.Environment
{
    public class HexGrid
    {
        private readonly IEnumerable<Point> XEvenNeighborModifiers = new List<Point>() {
            new Point(-1, -1),
            new Point(-1, 0),
            new Point(0, 1),
            new Point(1, 0),
            new Point(1, -1),
            new Point(0, -1)
        };

        private readonly IEnumerable<Point> XOddNeighborModifiers = new List<Point>() {
            new Point(-1, 0),
            new Point(-1, 1),
            new Point(0, 1),
            new Point(1, 1),
            new Point(1, 0),
            new Point(0, -1)
        };

        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public HexGrid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public IEnumerable<Point> GetNeighbors(int x, int y)
        {
            if(!IsInsideGrid(x, y))
            {
                throw new ArgumentException($"The provided coordinates {nameof(x)}, {nameof(y)} are outside this grid.\n" +
                    $"\t{nameof(x)}-coordinate must be between 0 and {Width - 1}.\n" +
                    $"\t{nameof(y)}-coordinate must be between 0 and {Height - 1}.");
            }

            List<Point> neighbors = new List<Point>();

            foreach(var m in GetNeighborIndexModifiers(x, y))
            {
                if(IsInsideGrid(x + m.X, y + m.Y))
                {
                    neighbors.Add(new Point(x + m.X, y + m.Y));
                }
            }

            return neighbors;

        }

        private bool IsInsideGrid(int x, int y)
        {
            return 0 <= x && x < Width && 0 <= y && y < Height;
        }

        private IEnumerable<Point> GetNeighborIndexModifiers(int x, int y)
        {
            if (x % 2 == 0)
            {
                return XEvenNeighborModifiers;
            }
            else
            {
                return XOddNeighborModifiers;
            }
        }
    }
}
