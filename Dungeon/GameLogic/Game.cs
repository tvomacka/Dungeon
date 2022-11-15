using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Game
    {
        private static Game instance = null;

        public Dictionary<string, Character> Characters { get; set; }

        public Party Party { get; set; }
        public List<Quest> Quests { get; set; }
        public IInteraction State { get; set; }

        private Game()
        {
            Characters = new Dictionary<string, Character>();
            Quests = new List<Quest>();
            Party = new Party();
        }

        public IInteraction InteractWith(Character npc)
        {
            return npc.Interact();
        }

        public void Load(string path)
        {
            Characters["QuestNPC"] = new Character() 
            {
                Location = new Point(5, 5) 
            };
            Party.Location = new Point(3, 5);
        }

        public static Game Instance
        {
            get 
            { 
                instance ??= new Game();
                return instance;
            }
        }

        public static IInteraction Explore { get; internal set; }
    }
}
