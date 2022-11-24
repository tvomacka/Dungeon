using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Dungeon.GameLogic;
using static System.Windows.Forms.Design.AxImporter;

namespace Dungeon.Dialogues
{
    public partial class Dialogue : IInteraction
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int InitialState { get; set; }

        private int state;

        public DialogueState[] States { get; set; }

        public Dialogue StartDialogue()
        {
            state = InitialState;
            return this;
        }

        public DialogueState GetCurrentState()
        {
            return States[state];
        }

        public IInteraction ChooseOption(int v)
        {
            var option = GetCurrentState().Options[v];
            if (option.Condition != null && !option.Condition.IsSatisfied())
            {
                throw new ArgumentException($"You are trying to choose a dialog option with unsatisfied condition.\n\tOption: {option}\n\tCondition: {option.Condition}");
            }
            if (option.Actions != null)
            {
                foreach (var action in option.Actions)
                {
                    action.Execute();
                }
            }
            var newState = option.TargetState;
            if (0 <= newState && newState < States.Length)
            {
                state = newState;
                return this;
            }

            return Game.ExploreState;
        }

        public override string ToString()
        {
            return "Dialogue";
        }

        public IEnumerable<DialogueOption> GetFilteredOptions()
        {
            var state = GetCurrentState();
            return state.Options.Where(o => o.Condition == null || o.Condition.IsSatisfied());
        }

        public class DialogueOption
        {
            public string Text { get; set; }
            public int TargetState { get; set; }

            public DialogueAction[] Actions { get; set; }

            public DialogueCondition Condition { get; set; }

            public override string ToString()
            {
                return $"{Text}";
            }
        }
    }

    public class DialogueCondition
    {
        public string Subject { get; set; }
        public string Test { get; set; }
        public string Target { get; set; }

        public bool IsSatisfied()
        {
            if (Subject == "Intelligence" && Test == "GreaterThan")
                return Game.Instance.Party[0].Intelligence > int.Parse(Target);
            else if (Subject == "Item" && Test == "InInventory")
                return Game.Instance.Party[0].Inventory.Contains(int.Parse(Target));

            return false;
        }

        public override string ToString()
        {
            return $"{Subject} {Test} {Target}";
        }
    }

    public class DialogueAction
    {
        public string ActionType { get; set; }
        public string[] ActionParameters { get; set; }

        public void Execute()
        {
            if (ActionType == "AcceptQuest")
                Game.Instance.Party.ActiveQuests.Add(int.Parse(ActionParameters[0]));
            else if (ActionType == "FinishQuest")
                Game.Instance.Party.ActiveQuests.Remove(int.Parse(ActionParameters[0]));
            else if (ActionType == "LoseItem")
                Game.Instance.Party[0].Inventory.Remove(int.Parse(ActionParameters[0]));
            else if (ActionType == "GainXP")
                Game.Instance.Party[0].AddXP(int.Parse(ActionParameters[0]));
        }
    }
}
