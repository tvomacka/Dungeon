using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Party
    {
        public Point Location { get; set; }
        public List<PlayerCharacter> Members { get; set; }
        public PlayerCharacter this[int index]
        {
            get { return Members[index]; }
        }
        public List<int> AssignedQuests { get; set; }

        public Party()
        {
            Members = new List<PlayerCharacter>();
            AssignedQuests = new List<int>();
        }

        public void MoveTo(int x, int y)
        {
            Location = new Point(x, y);
        }

        public void MoveTo(Point location)
        {
            MoveTo(location.X, location.Y);
        }
    }
}
