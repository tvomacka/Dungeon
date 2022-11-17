using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Character
    {
        public Point Location { get; set; }

        internal Dialogue StartDialogue()
        {
            return new Dialogue()
            {
                Text = "Do you want to accept the quest?",
                Options = new List<string>()
                {
                    "Yes",
                    "No"
                }
            };
        }
    }
}
