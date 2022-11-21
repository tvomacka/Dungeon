namespace Dungeon.GameLogic
{
    public class PlayerCharacter
    {
        public string Name { get; set; }
        public int Intelligence { get; set; }
        public List<int> Inventory { get; set; }

        public PlayerCharacter()
        {
            Inventory = new List<int>();
        }
    }
}