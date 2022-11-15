using Dungeon.GameLogic;

namespace DungeonTests
{
    [TestClass]
    public class GameLogicTests
    {
        private Game game = new Game();

        [TestMethod]
        public void Party_CanMoveTowardsNpc()
        {
            game.Load(@"TestResources\Games\test.game");
            Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());

            var npc = game.Characters["QuestNPC"];
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);

            Assert.AreEqual("{X=4,Y=5}", game.Party.Location.ToString());
        }



        [TestMethod]
        public void Party_CanGetQuestFromNpcThroughDialogue()
        {
            game.Load(@"TestResources\Games\test.game");
            Assert.AreEqual(0, game.Quests.Count);

            var npc = game.Characters["QuestNPC"];
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
            game.State = game.InteractWith(npc);
            game.State = (game.State as Dialogue).ChooseOption(1);

            Assert.AreEqual(1, game.Quests.Count);
        }
    }
}