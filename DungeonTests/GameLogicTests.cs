using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using Dungeon.GameLogic;

namespace DungeonTests
{
    [UseReporter(typeof(VisualStudioReporter))]
    [TestClass]
    public class GameLogicTests
    {
        private Game game = Game.Instance;

        private void LoadTestGame(string gameName)
        {
            game.Load(@"..\..\..\TestResources\Games\" + gameName);
        }


        [TestMethod]
        public void PartyLocation_CanBeLoadedFromFile()
        {
            LoadTestGame("test.json");
            Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());
        }

        [TestMethod]
        public void PartyCharacter_CanBeLoadedFromFile()
        {
            LoadTestGame("party.json");
            Assert.AreEqual(1, game.Party.Members.Count());
        }

        [TestMethod]
        public void LoadedPlayerCharacter_HasCorrectInt()
        {
            LoadTestGame("party.json");
            Assert.AreEqual(10, game.Party.Members[0].Intelligence);
        }

        [TestMethod]
        public void Party_CanMoveTowardsNpc()
        {
            LoadTestGame("test.json");
            Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());

            var npc = game.GetCharacter("QuestNPC");
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);

            Assert.AreEqual("{X=4,Y=5}", game.Party.Location.ToString());
        }

        [TestMethod]
        public void Party_CanGetQuestFromNpcThroughDialogue()
        {
            LoadTestGame("test.json");
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
            LoadTestGame("test.json");
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
            LoadTestGame("test.json");

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
            LoadTestGame("dialogueCondition.json");

            var npc = game.GetCharacter("DialogueNPC");
            game.State = game.DialogueWith(npc);
            Approvals.VerifyAll((game.State as Dialogue).GetFilteredOptions(), "");
        }

        [TestMethod]
        public void DialogueOptionWithCondition_ShowsWhenConditionIsMet()
        {
            LoadTestGame("dialogueCondition.json");
            game.Party.Members[0].Intelligence = 100;

            var npc = game.GetCharacter("DialogueNPC");
            game.State = game.DialogueWith(npc);
            Approvals.VerifyAll((game.State as Dialogue).GetFilteredOptions(), "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Player_CannotChoose_DialogueOptionWithUnsatisfiedCondition()
        {
            LoadTestGame("dialogueCondition.json");

            var npc = game.GetCharacter("DialogueNPC");
            game.State = game.DialogueWith(npc);
            (game.State as Dialogue).ChooseOption(2);
        }

        [TestMethod]
        public void Item_CanBeLoaded_FromJson()
        {
            LoadTestGame("fetchQuest.json");

            Assert.AreEqual(1, game.Items.Count);
            Assert.AreEqual("Item 0: Quest Item", game.Items[0].ToString());
        }

        [TestMethod]
        public void Quest_CanBeLoaded_FromJson()
        {
            LoadTestGame("fetchQuest.json");

            Assert.AreEqual(1, game.Quests.Count);
            Assert.AreEqual("Quest 0: Fetch Quest Test", game.Quests[0].ToString());
        }
    }
}