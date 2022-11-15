using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Game
    {
        public Level CurrentLevel { get; set; }
        public Dictionary<string, Character> Characters { get; set; }

        public Party Party { get; set; }
        public List<Quest> Quests { get; set; }

        public IInteraction InteractWith(Character npc)
        {
            throw new NotImplementedException();
        }
    }
}
