using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Game
    {
        private static Game instance = null;

        private List<Character> Characters { get; set; }

        public Party Party { get; set; }
        public List<Quest> Quests { get; set; }
        public IInteraction State { get; set; }

        private Game()
        {
            Characters = new List<Character>();
            Quests = new List<Quest>();
            Party = new Party();
            State = new Explore();
        }

        public IInteraction DialogueWith(Character npc)
        {
            return npc.StartDialogue();
        }

        public void Load(string path)
        {
            string jsonString = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<JsonObject>(jsonString);

            Characters = JsonSerializer.Deserialize<Character[]>(json["Characters"]).ToList<Character>();

            //Characters["QuestNPC"] = new Character() 
            //{
            //    Location = new Point(5, 5) 
            //};
            //Characters["DialogueNPC"] = new Character()
            //{
            //    Location = new Point(4, 5)
            //};

            //Quests = new List<Quest>();
            Party = JsonSerializer.Deserialize<Party>(json["Party"]);
        }

        public Character GetCharacter(string name)
        {
            return Characters.Single(c => c.Name.Equals(name));
        }

        public static Game Instance
        {
            get 
            { 
                instance ??= new Game();
                return instance;
            }
        }

        public class Explore : IInteraction
        {
            public override string ToString()
            {
                return "Explore";
            }
        }
    }
}
