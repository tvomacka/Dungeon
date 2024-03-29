﻿using Dungeon.GameLogic.Exceptions;
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
            switch (test)
            {
                case "GreaterThan":
                    return (x, y) => { return (int)x > y; };
                case "GreaterThanOrEqual":
                    return (x, y) => { return (int)x >= y; };
                case "LessThan":
                    return (x, y) => { return (int)x < y; };
                case "LessThanOrEqual":
                    return (x, y) => { return (int)x <= y; };
                case "Equal":
                    return (x, y) => { return (int)x == y; };
                case "NotEqual":
                    return (x, y) => { return (int)x != y; };
                case "Contains":
                    return (x, y) => { return (x as IList).Contains(y); };
            }

            throw new GameException($"There is no comparator for the provided test value {test}.");
        }

        private int GetTargetValue(string target)
        {
            return int.Parse(target);
        }

        public static object GetSubjectValue(PlayerCharacter playerCharacter, string subject)
        {
            switch (subject)
            {
                case "Intelligence":
                    return playerCharacter.Intelligence;
                case "Strength":
                    return playerCharacter.Strength;
                case "Inventory":
                    return playerCharacter.Inventory.Select(i => i.Id).ToList();
                case "AssignedQuests":
                    return Game.Instance.Party.AssignedQuests;
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
