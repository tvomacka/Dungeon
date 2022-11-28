namespace Dungeon.GameLogic
{
    public class PlayerCharacter
    {
        #region General
        public string Name { get; set; }
        public int XP { get; set; }
        public List<int> Inventory { get; set; }
        #endregion

        #region Attributes
        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Health { get; set; }
        #endregion

        public PlayerCharacter()
        {
            Inventory = new List<int>();
        }

        public void AddXP(int v)
        {
            XP += v;
        }
    }
}