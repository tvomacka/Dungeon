namespace Dungeon.Dialogues
{
    public partial class Dialogue
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
}
