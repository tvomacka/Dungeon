using Dungeon.GameLogic.Equipment;
using Dungeon.GameLogic.Exceptions;

namespace Dungeon.GameLogic
{
    public class PlayerCharacter
    {
        #region General
        public string Name { get; set; }
        public int XP { get; set; }
        #endregion

        #region Attributes
        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Health { get; set; }
        #endregion

        #region Equipment and Inventory
        public List<int> Inventory { get; set; }
        public EquipmentSlot RightHand { get; set; }
        #endregion

        public PlayerCharacter()
        {
            Inventory = new List<int>();
            RightHand = new EquipmentSlot();
        }

        public void AddXP(int v)
        {
            XP += v;
        }

        public void Equip(int itemId, EquipmentSlot target)
        {
            var item = Game.Instance.Items.Single(i => i.Id == itemId);
            
            if (item is Weapon && (item as Weapon).MinStrength > Strength)
                throw new GameException("Attempting to equip a weapon with insufficient strength.\n" + 
                    $"{item.Name} requires {(item as Weapon).MinStrength} strength\n" +
                    $"{Name} has {Strength} strength");

            target.Equip(itemId);
        }
    }
}