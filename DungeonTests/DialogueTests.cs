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
    [TestMethod]
    public void Party_CanGetQuestFromNpcThroughDialogue()
    {
        GameLogicTests.LoadTestGame("test.json");
        Assert.AreEqual(0, Game.Instance.Party.ActiveQuests.Count);

        var npc = Game.Instance.GetCharacter("QuestNPC");
        Game.Instance.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        Game.Instance.State = Game.Instance.DialogueWith(npc);
        Game.Instance.State = (Game.Instance.State as Dialogue).ChooseOption(0);

        Assert.AreEqual(1, Game.Instance.Party.ActiveQuests.Count);
    }



    [TestMethod]
    public void Party_CanDeclineQuestFromNpcThroughDialogue()
    {
        GameLogicTests.LoadTestGame("test.json");
        Assert.AreEqual(0, Game.Instance.Party.ActiveQuests.Count);

        var npc = Game.Instance.GetCharacter("QuestNPC");
        Game.Instance.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        Game.Instance.State = Game.Instance.DialogueWith(npc);
        Game.Instance.State = (Game.Instance.State as Dialogue).ChooseOption(1);

        Assert.AreEqual(0, Game.Instance.Party.ActiveQuests.Count);
    }

    [TestMethod]
    public void Dialogue_CanTraverseFromGreetingsToText()
    {
        GameLogicTests.LoadTestGame("test.json");

        var states = new List<string>();

        var npc = Game.Instance.GetCharacter("DialogueNPC");
        Game.Instance.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        Game.Instance.State = Game.Instance.DialogueWith(npc);
        states.Add((Game.Instance.State as Dialogue).GetCurrentState().ToString());
        Game.Instance.State = (Game.Instance.State as Dialogue).ChooseOption(0);
        states.Add((Game.Instance.State as Dialogue).GetCurrentState().ToString());
        Game.Instance.State = (Game.Instance.State as Dialogue).ChooseOption(0);

        Approvals.VerifyAll(states, "");
    }

    [TestMethod]
    public void DialogueOptionWithCondition_DoesNotShowByDefault()
    {
        GameLogicTests.LoadTestGame("dialogueCondition.json");

        var npc = Game.Instance.GetCharacter("DialogueNPC");
        Game.Instance.State = Game.Instance.DialogueWith(npc);
        Approvals.VerifyAll((Game.Instance.State as Dialogue).GetFilteredOptions(), "");
    }

    [TestMethod]
    public void DialogueOptionWithCondition_ShowsWhenConditionIsMet()
    {
        GameLogicTests.LoadTestGame("dialogueCondition.json");
        Game.Instance.Party[0].Intelligence = 100;

        var npc = Game.Instance.GetCharacter("DialogueNPC");
        Game.Instance.State = Game.Instance.DialogueWith(npc);
        Approvals.VerifyAll((Game.Instance.State as Dialogue).GetFilteredOptions(), "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Player_CannotChoose_DialogueOptionWithUnsatisfiedCondition()
    {
        GameLogicTests.LoadTestGame("dialogueCondition.json");

        var npc = Game.Instance.GetCharacter("DialogueNPC");
        Game.Instance.State = Game.Instance.DialogueWith(npc);
        (Game.Instance.State as Dialogue).ChooseOption(2);
    }

    [TestMethod]
    public void DialogueCondition_CanTarget_StrengthAttribute()
    {
        GameLogicTests.ResetGame();

        var condition = new DialogueCondition
        {
            Subject = "Strength",
            Test = "GreaterThan",
            Target = "10"
        };

        var pc = new PlayerCharacter() { Strength = 11 };

        Assert.AreEqual(true, condition.IsSatisfied(pc));
    }

    [TestMethod]
    public void DialogueCondition_AssignedQuest_CanBeTested()
    {
        var assignedQuestComparator = DialogueCondition.GetComparator("Assigned");
        Assert.IsTrue(assignedQuestComparator(1, 1));
    }

    [TestMethod]
    public void DialogueCondition_NumericComparators_ReturnExpectedResults()
    {
        var greaterThanComparator = DialogueCondition.GetComparator("GreaterThan");
        Assert.IsTrue(greaterThanComparator(10, 1));
        Assert.IsFalse(greaterThanComparator(10, 10));
        Assert.IsFalse(greaterThanComparator(10, 11));

        var greaterThanOrEqualComparator = DialogueCondition.GetComparator("GreaterThanOrEqual");
        Assert.IsTrue(greaterThanOrEqualComparator(10, 1));
        Assert.IsTrue(greaterThanOrEqualComparator(10, 10));
        Assert.IsFalse(greaterThanOrEqualComparator(10, 11));

        var lessThanComparator = DialogueCondition.GetComparator("LessThan");
        Assert.IsTrue(lessThanComparator(1, 10));
        Assert.IsFalse(lessThanComparator(10, 10));
        Assert.IsFalse(lessThanComparator(11, 10));

        var lessThanOrEqualComparator = DialogueCondition.GetComparator("LessThanOrEqual");
        Assert.IsTrue(lessThanOrEqualComparator(1, 10));
        Assert.IsTrue(lessThanOrEqualComparator(10, 10));
        Assert.IsFalse(lessThanOrEqualComparator(11, 10));

        var equalComparator = DialogueCondition.GetComparator("Equal");
        Assert.IsFalse(equalComparator(1, 10));
        Assert.IsTrue(equalComparator(10, 10));
        Assert.IsFalse(equalComparator(11, 10));

        var notEqualComparator = DialogueCondition.GetComparator("NotEqual");
        Assert.IsTrue(notEqualComparator(1, 10));
        Assert.IsFalse(notEqualComparator(10, 10));
        Assert.IsTrue(notEqualComparator(11, 10));
    }
}
