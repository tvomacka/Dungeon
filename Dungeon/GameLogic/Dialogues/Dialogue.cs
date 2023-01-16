namespace Dungeon.GameLogic.Dialogues;

public class Dialogue : IInteraction
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
        if (option.Condition != null && !option.Condition.IsSatisfied(Game.Instance.Party[0]))
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
        return state.Options.Where(o => o.Condition == null || o.Condition.IsSatisfied(Game.Instance.Party[0]));
    }
}
