﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Party
    {
        public Point Location { get; set; }

        public void MoveTo(int x, int y)
        {
            Location = new Point(x, y);
        }
    }
}
