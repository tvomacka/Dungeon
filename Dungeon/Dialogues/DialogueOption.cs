namespace Dungeon.Dialogues
{
    public partial class Dialogue
    {
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
}
