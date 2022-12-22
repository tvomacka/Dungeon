using Dungeon.GameLogic.Exceptions;
using System.Collections;

namespace Dungeon.GameLogic.Dialogues
{
    public class DialogueCondition
    {
        public string Subject { get; set; }
        public string Test { get; set; }
        public string Target { get; set; }

        public bool IsSatisfied(PlayerCharacter playerCharacter)
        {
            return Compare(GetSubjectValue(playerCharacter, Subject), GetTargetValue(Target), GetComparator(Test));
        }

        public static Func<object, int, bool> GetComparator(string test)
        {
            if (test == "GreaterThan")
            {
                return (x, y) => { return (int)x > y; };
            }
            else if (test == "GreaterThanOrEqual")
            {
                return (x, y) => { return (int)x >= y; };
            }
            else if (test == "LessThan")
            {
                return (x, y) => { return (int)x < y; };
            }
            else if (test == "LessThanOrEqual")
            {
                return (x, y) => { return (int)x <= y; };
            }
            else if (test == "Equal")
            {
                return (x, y) => { return (int)x == y; };
            }
            else if (test == "NotEqual")
            {
                return (x, y) => { return (int)x != y; };
            }
            else if(test == "Contains")
            {
                //TODO: we don't want to be calling Game.Instance... stuff here
                return (x, y) => { return (x as IList).Contains(y); };
            }

            throw new GameException($"There is no comparator for the provided test value {test}.");
        }

        private int GetTargetValue(string target)
        {
            return int.Parse(target);
        }

        private object GetSubjectValue(PlayerCharacter playerCharacter, string subject)
        {
            if(subject == "Intelligence")
            {
                return playerCharacter.Intelligence;
            }
            else if (subject == "Strength")
            {
                return playerCharacter.Strength;
            }
            else if (subject == "Inventory")
            {
                return playerCharacter.Inventory.Select(i => i.Id).ToList();
            }

            throw new GameException($"You are trying to get {subject} value from {playerCharacter}.\n"
                + $"It cannot be obtained using this method.");
        }

        public bool Compare(object value, int target, Func<object, int, bool> comparator)
        {
            return comparator(value, target);
        }

        public override string ToString()
        {
            return $"{Subject} {Test} {Target}";
        }
    }
}
