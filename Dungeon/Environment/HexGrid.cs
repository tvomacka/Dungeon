using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.Environment
{
    public class HexGrid
    {
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
            if (x % 2 == 0)
            {
                return new List<Point>() {
                    new Point(x - 1, y - 1),
                    new Point(x - 1, y),
                    new Point(x, y + 1),
                    new Point(x + 1, y),
                    new Point(x + 1, y - 1),
                    new Point(x, y - 1)
                };
            }
            else
            {
                return new List<Point>() {
                    new Point(x - 1, y),
                    new Point(x - 1, y + 1),
                    new Point(x, y + 1),
                    new Point(x + 1, y + 1),
                    new Point(x + 1, y),
                    new Point(x, y - 1)
                };
            }
        }
    }
}
