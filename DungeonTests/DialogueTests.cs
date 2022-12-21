using Dungeon.GameLogic;
using Dungeon.GameLogic.Dialogues;

namespace DungeonTests;

[TestClass]
public class DialogueTests
{
    private Game game = Game.Instance;

    [TestMethod]
    public void Party_CanGetQuestFromNpcThroughDialogue()
    {
        GameLogicTests.LoadTestGame("test.json");
        Assert.AreEqual(0, game.Party.ActiveQuests.Count);

        var npc = game.GetCharacter("QuestNPC");
        game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        game.State = game.DialogueWith(npc);
        game.State = (game.State as Dialogue).ChooseOption(0);

        Assert.AreEqual(1, game.Party.ActiveQuests.Count);
    }
}
