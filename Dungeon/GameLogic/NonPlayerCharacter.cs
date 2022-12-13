using Dungeon.GameLogic.Dialogues;

namespace Dungeon.GameLogic;

public class NonPlayerCharacter
{
    public Point Location { get; set; }
    public string Name { get; set; }
    public int Dialogue { get; set; }

    internal Dialogue StartDialogue()
    {
        return Game.Instance.Dialogues.Single(d => d.Id == Dialogue);
    }
}
