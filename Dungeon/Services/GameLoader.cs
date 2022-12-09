using Dungeon.GameLogic.Dialogues;
using Dungeon.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using Dungeon.GameLogic.Equipment;

namespace Dungeon.Services
{
    public class GameLoader
    {
        public static Game Load(string path)
        {
            string jsonString = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<JsonObject>(jsonString);

            var game = new Game();

            if (json["Characters"] != null)
            {
                game.Characters = JsonSerializer.Deserialize<NonPlayerCharacter[]>(json["Characters"]).ToList<NonPlayerCharacter>();
            }
            if (json["Dialogues"] != null)
            {
                game.Dialogues = JsonSerializer.Deserialize<Dialogue[]>(json["Dialogues"]).ToList<Dialogue>();
            }
            if (json["Party"] != null)
            {
                game.Party = JsonSerializer.Deserialize<Party>(json["Party"]);
            }
            if (json["Items"] != null)
            {
                game.Items = JsonSerializer.Deserialize<Item[]>(json["Items"]).ToList<Item>();
            }
            if (json["Quests"] != null)
            {
                game.Quests = JsonSerializer.Deserialize<Quest[]>(json["Quests"]).ToList<Quest>();
            }

            return game;
        }
    }
}
