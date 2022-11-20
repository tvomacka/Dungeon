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
            Assert.AreEqual(0, game.Quests.Count);

            var npc = game.GetCharacter("QuestNPC");
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
            game.State = game.DialogueWith(npc);
            game.State = (game.State as Dialogue).ChooseOption(0);

            Assert.AreEqual(1, game.Quests.Count);
        }

        [TestMethod]
        public void Party_CanDeclineQuestFromNpcThroughDialogue()
        {
            LoadTestGame("test.json");
            Assert.AreEqual(0, game.Quests.Count);

            var npc = game.GetCharacter("QuestNPC");
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
            game.State = game.DialogueWith(npc);
            game.State = (game.State as Dialogue).ChooseOption(1);

            Assert.AreEqual(0, game.Quests.Count);
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
    }
}