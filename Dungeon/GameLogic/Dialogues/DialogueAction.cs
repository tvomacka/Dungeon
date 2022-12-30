using Dungeon.GameLogic;

namespace Dungeon.GameLogic.Dialogues
{
    public class DialogueAction
    {
        public string ActionType { get; set; }
        public string[] ActionParameters { get; set; }

        public void Execute()
        {
            if (ActionType == "AcceptQuest")
                Game.Instance.Party.AssignedQuests.Add(int.Parse(ActionParameters[0]));
            else if (ActionType == "CompleteQuest")
                Game.Instance.Party.AssignedQuests.Remove(int.Parse(ActionParameters[0]));
            else if (ActionType == "LoseItem")
                Game.Instance.Party[0].Inventory.Remove(Game.Instance.Items.Single(i => i.Id == int.Parse(ActionParameters[0])));
            else if (ActionType == "GainXP")
                Game.Instance.Party[0].AddXP(int.Parse(ActionParameters[0]));
        }
    }
}
