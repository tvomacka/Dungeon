using Dungeon.GameLogic.Equipment;

namespace Dungeon.GameLogic
{
    public class EquipmentSlot
    {
        private Item equipped = null;

        public bool IsEmpty()
        {
            return equipped == null;
        }

        public bool IsEquippedWith(Item item)
        {
            return equipped.Equals(item);
        }

        public void Equip(Item item)
        {
            equipped = item;
        }
    }
}