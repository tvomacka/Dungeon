using Dungeon.GameLogic;

namespace Dungeon.GameLogic.Dialogues
{
    public class DialogueCondition
    {
        public string Subject { get; set; }
        public string Test { get; set; }
        public string Target { get; set; }

        public bool IsSatisfied(PlayerCharacter playerCharacter)
        {
            if (Subject == "Intelligence" && Test == "GreaterThan")
                return playerCharacter.Intelligence > int.Parse(Target);
            else if (Subject == "Item" && Test == "InInventory")
                return Game.Instance.Party[0].Inventory.Contains(int.Parse(Target));

            return false;
        }

        public override string ToString()
        {
            return $"{Subject} {Test} {Target}";
        }
    }
}
