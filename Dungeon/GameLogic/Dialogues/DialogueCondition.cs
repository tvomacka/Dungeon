using Dungeon.GameLogic;

namespace Dungeon.GameLogic.Dialogues
{
    public class DialogueCondition
    {
        public string Subject { get; set; }
        public string Test { get; set; }
        public string Target { get; set; }

        public bool IsSatisfied()
        {
            if (Subject == "Intelligence" && Test == "GreaterThan")
                return Game.Instance.Party[0].Intelligence > int.Parse(Target);
            else if (Subject == "Inventory" && Test == "Contains")
                return Game.Instance.Party[0].Inventory.Contains(int.Parse(Target));

            return false;
        }

        public override string ToString()
        {
            return $"{Subject} {Test} {Target}";
        }
    }
}
