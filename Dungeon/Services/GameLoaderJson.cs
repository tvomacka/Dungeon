using Dungeon.GameLogic.Dialogues;
using Dungeon.GameLogic;
using System.Text.Json.Nodes;
using System.Text.Json;
using Dungeon.GameLogic.Equipment;

namespace Dungeon.Services;

public class GameLoaderJson
{
    public static void Load(string path)
    {
        string jsonString = File.ReadAllText(path);
        var json = JsonSerializer.Deserialize<JsonObject>(jsonString);

        if (json["Characters"] != null)
        {
            Game.Instance.Characters = JsonSerializer.Deserialize<NonPlayerCharacter[]>(json["Characters"]).ToList<NonPlayerCharacter>();
        }
        if (json["Dialogues"] != null)
        {
            Game.Instance.Dialogues = JsonSerializer.Deserialize<Dialogue[]>(json["Dialogues"]).ToList<Dialogue>();
        }
        if (json["Party"] != null)
        {
            Game.Instance.Party = JsonSerializer.Deserialize<Party>(json["Party"]);
        }
        if (json["Items"] != null)
        {
            Game.Instance.Items = JsonSerializer.Deserialize<Item[]>(json["Items"]).ToList<Item>();
        }
        if (json["Quests"] != null)
        {
            Game.Instance.Quests = JsonSerializer.Deserialize<Quest[]>(json["Quests"]).ToList<Quest>();
        }
    }
}
