using ApprovalTests;
using ApprovalTests.Reporters.Windows;
using ApprovalTests.Reporters;
using Dungeon.GameLogic;
using Dungeon.GameLogic.Dialogues;

namespace DungeonTests;

[TestClass]
[UseReporter(typeof(VisualStudioReporter))]
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



    [TestMethod]
    public void Party_CanDeclineQuestFromNpcThroughDialogue()
    {
        GameLogicTests.LoadTestGame("test.json");
        Assert.AreEqual(0, game.Party.ActiveQuests.Count);

        var npc = game.GetCharacter("QuestNPC");
        game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        game.State = game.DialogueWith(npc);
        game.State = (game.State as Dialogue).ChooseOption(1);

        Assert.AreEqual(0, game.Party.ActiveQuests.Count);
    }

    [TestMethod]
    public void Dialogue_CanTraverseFromGreetingsToText()
    {
        GameLogicTests.LoadTestGame("test.json");

        var states = new List<string>();

        var npc = game.GetCharacter("DialogueNPC");
        game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        game.State = game.DialogueWith(npc);
        states.Add((game.State as Dialogue).GetCurrentState().ToString());
        game.State = (game.State as Dialogue).ChooseOption(0);
        states.Add((game.State as Dialogue).GetCurrentState().ToString());
        game.State = (game.State as Dialogue).ChooseOption(0);

        Approvals.VerifyAll(states, "");
    }

    [TestMethod]
    public void DialogueOptionWithCondition_DoesNotShowByDefault()
    {
        GameLogicTests.LoadTestGame("dialogueCondition.json");

        var npc = game.GetCharacter("DialogueNPC");
        game.State = game.DialogueWith(npc);
        Approvals.VerifyAll((game.State as Dialogue).GetFilteredOptions(), "");
    }

    [TestMethod]
    public void DialogueOptionWithCondition_ShowsWhenConditionIsMet()
    {
        GameLogicTests.LoadTestGame("dialogueCondition.json");
        game.Party[0].Intelligence = 100;

        var npc = game.GetCharacter("DialogueNPC");
        game.State = game.DialogueWith(npc);
        Approvals.VerifyAll((game.State as Dialogue).GetFilteredOptions(), "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Player_CannotChoose_DialogueOptionWithUnsatisfiedCondition()
    {
        GameLogicTests.LoadTestGame("dialogueCondition.json");

        var npc = game.GetCharacter("DialogueNPC");
        game.State = game.DialogueWith(npc);
        (game.State as Dialogue).ChooseOption(2);
    }

    [TestMethod]
    public void DialogueCondition_CanTarget_StrengthAttribute()
    {
        GameLogicTests.ResetGame();
    }
}
