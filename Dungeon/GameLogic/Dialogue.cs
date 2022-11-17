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

        public DialogueState[] States { get; set; }

        public IInteraction ChooseOption(int v)
        {
            if (v == 0)
            {
                Game.Instance.Quests.Add(new Quest());
            }
            return new Game.Explore();
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
