using Dungeon.GameLogic.Dialogues;
using Dungeon.GameLogic;
using System.Text.Json.Nodes;
using System.Text.Json;
using Dungeon.GameLogic.Equipment;

namespace Dungeon.Services
{
    public class GameLoader
    {
        public static void Load(Game game, string path) 
        {
            string jsonString = File.ReadAllText(path);
            var json = JsonSerializer.Deserialize<JsonObject>(jsonString);

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
        }
    }
}
