using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon.GameLogic
{
    public class Dialogue : IInteraction
    {
        public IInteraction ChooseOption(int v)
        {
            Game.Instance.Quests.Add(new Quest());
            return Game.Explore;
        }
    }
}
