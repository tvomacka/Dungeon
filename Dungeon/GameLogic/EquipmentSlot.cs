using Dungeon.GameLogic.Equipment;

namespace Dungeon.GameLogic
{
    public class EquipmentSlot
    {
        private int equippedId = -1;

        public bool IsEmpty()
        {
            return equippedId == -1;
        }

        public bool IsEquippedWith(Item item)
        {
            return equippedId == item.Id;
        }

        public void Equip(int itemId)
        {
            equippedId = itemId;
        }
    }
}