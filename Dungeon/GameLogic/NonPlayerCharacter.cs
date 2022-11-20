using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class NonPlayerCharacter
    {
        public Point Location { get; set; }
        public string Name { get; set; }
        public int Dialogue { get; set; }

        internal Dialogue StartDialogue()
        {
            return Game.Instance.Dialogues.Single(d => d.Id == Dialogue);
        }
    }
}
