using Dungeon.GameLogic;

namespace DungeonTests
{
    [TestClass]
    public class GameLogicTests
    {
        private Game game = Game.Instance;

        [TestMethod]
        public void PartyLocation_CanBeLoadedFromFile()
        {
            LoadTestGame();
            Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());
        }

        private void LoadTestGame()
        {
            game.Load(@"..\..\..\TestResources\Games\test.json");
        }

        [TestMethod]
        public void Party_CanMoveTowardsNpc()
        {
            LoadTestGame();
            Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());

            var npc = game.GetCharacter("QuestNPC");
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);

            Assert.AreEqual("{X=4,Y=5}", game.Party.Location.ToString());
        }

        [TestMethod]
        public void Party_CanGetQuestFromNpcThroughDialogue()
        {
            LoadTestGame();
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
            LoadTestGame();
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
            LoadTestGame();

            var npc = game.GetCharacter("DialogueNPC");
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
            game.State = game.DialogueWith(npc);
            Assert.AreEqual("Dialogue", game.State.ToString());
            game.State = (game.State as Dialogue).ChooseOption(0);
            Assert.AreEqual("Dialogue", game.State.ToString());
            game.State = (game.State as Dialogue).ChooseOption(0);
            Assert.AreEqual("Explore", game.State.ToString());
        }
    }
}