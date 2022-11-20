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
        }

        public class DialogueOption
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public int TargetState { get; set; }
        }
    }
}
