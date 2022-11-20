using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var newState = GetCurrentState().Options[v].TargetState;
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

            public override string ToString()
            {
                return $"{Text}";
            }
        }
    }
}
