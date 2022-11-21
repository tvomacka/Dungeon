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

        private List<NonPlayerCharacter> Characters { get; set; }
        public List<Dialogue> Dialogues { get; set; }
        public Party Party { get; set; }
        public List<Quest> Quests { get; set; }

        public List<Item> Items { get; set; }
        public IInteraction State { get; set; }

        private Game()
        {
            Characters = new List<NonPlayerCharacter>();
            Quests = new List<Quest>();
            Party = new Party();
            State = ExploreState;
        }

        public IInteraction DialogueWith(NonPlayerCharacter npc)
        {
            return npc.StartDialogue();
        }

        public void Load(string path)
        {
            string jsonString = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<JsonObject>(jsonString);

            if (json["Characters"] != null)
            {
                Characters = JsonSerializer.Deserialize<NonPlayerCharacter[]>(json["Characters"]).ToList<NonPlayerCharacter>();
            }
            if (json["Dialogues"] != null)
            {
                Dialogues = JsonSerializer.Deserialize<Dialogue[]>(json["Dialogues"]).ToList<Dialogue>();
            }
            if (json["Party"] != null)
            {
                Party = JsonSerializer.Deserialize<Party>(json["Party"]);
            }
            if (json["Items"] != null)
            {
                Items = JsonSerializer.Deserialize<Item[]>(json["Items"]).ToList<Item>();
            }
        }

        public NonPlayerCharacter GetCharacter(string name)
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

        public static readonly IInteraction ExploreState = new Explore();

        public class Explore : IInteraction
        {
            public override string ToString()
            {
                return "Explore";
            }
        }
    }
}
