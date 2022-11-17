using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Dialogue : IInteraction
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }

        private int State = InitialState;
        private static readonly int InitialState = 0;

        public IInteraction ChooseOption(int v)
        {
            if (v == 0)
            {
                Game.Instance.Quests.Add(new Quest());
            }
            return Game.Explore;
        }
    }
}
