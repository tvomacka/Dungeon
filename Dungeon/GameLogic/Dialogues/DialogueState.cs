﻿using static Dungeon.GameLogic.Dialogues.Dialogue;

namespace Dungeon.GameLogic.Dialogues
{
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
                text += $"\n\t{optionId++}: {option}";
            }
            return text;
        }
    }
}
