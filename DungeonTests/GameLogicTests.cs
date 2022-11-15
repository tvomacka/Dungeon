using Dungeon.GameLogic;

namespace DungeonTests
{
    [TestClass]
    public class GameLogicTests
    {
        private Game game = new Game();
        
        [TestMethod]
        public void Party_CanGetQuestFromNPC()
        {
            game.CurrentLevel = Level.LoadFile("testLevel.map");
            var npc = game.Characters["QuestNPC"];
            game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
            var dialogue = game.InteractWith(npc) as Dialogue;
            dialogue.ChooseOption(1);
            Assert.AreEqual(1, game.Quests.Count);
        }
    }
}