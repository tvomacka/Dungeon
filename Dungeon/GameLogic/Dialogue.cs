using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;

namespace Dungeon.GameLogic
{
    public class Dialogue : IInteraction
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int InitialState { get; set; }

        private int state;

        public DialogueState[] States { get; set; }

        public Dialogue StartDialogue()
        {
            this.state = InitialState;
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
            if(option.Actions != null)
            {
                foreach (var action in option.Actions)
                {
                    action.Execute();
                }
            }
            var newState = option.TargetState;
            if (0 <= newState && newState < States.Length)
            {
                this.state = newState;
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

        public class DialogueState
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public DialogueOption[] Options { get; set; }

            public override string ToString()
            {
                var text = Text;
                var optionId = 0;
                foreach (var option in Options)
                {
                    text += ($"\n\t{optionId++}: {option}");
                }
                return text;
            }
        }

        public class DialogueOption
        {
            public string Text { get; set; }
            public int TargetState { get; set; }

            public DialogAction[] Actions { get; set; }

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
            return Game.Instance.Party.Members[0].Intelligence > int.Parse(Target);
        }

        public override string ToString()
        {
            return $"{Subject} {Test} {Target}";
        }
    }

    public class DialogAction
    {
        public string ActionType { get; set; }
        public string[] ActionParameters {get; set;}

        public void Execute()
        {
            Game.Instance.Party.ActiveQuests.Add(new Quest());
        }
    }
}
