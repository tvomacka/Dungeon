using Dungeon.GameLogic.Dialogues;
using Dungeon.GameLogic.Equipment;
using Dungeon.GameLogic.Exceptions;
using Dungeon.Services;

namespace Dungeon.GameLogic
{
    public class Game
    {
        private static Game instance = null;

        public List<NonPlayerCharacter> Characters { get; set; }
        public List<Dialogue> Dialogues { get; set; }
        public Party Party { get; set; }
        public List<Quest> Quests { get; set; }

        public List<Item> Items { get; set; }
        public IInteraction State { get; set; }

        private Game()
        {
            Characters = new();
            Quests = new();
            Party = new();
            Items = new();
            State = ExploreState;
        }

        public IInteraction DialogueWith(NonPlayerCharacter npc)
        {
            return npc.StartDialogue();
        }
        public void Reset()
        {
            instance = new Game();
        }

        public void Load(string path)
        {
            GameLoader.Load(this, path);
        }

        public NonPlayerCharacter GetCharacter(string name)
        {
            return Characters.Single(c => c.Name.Equals(name));
        }

        public void PickUpItem(int itemId, int characterId)
        {
            if (itemId < 0 || Items?.Count <= itemId)
                throw new GameException($"Trying to pick up an item with invalid id: {itemId}. There are currently {Items?.Count} items registered in the game.");
            if (characterId < 0 || Party.Members?.Count <= characterId)
                throw new GameException($"You are trying to give item {itemId} to a character with invalid id {characterId}. There are currently {Party.Members?.Count} characters in the party.");
            
            var item = Items[itemId];
            var character = Party[characterId];

            if(Party.Location.Equals(item.Location))
            {
                character.Inventory.Add(Game.Instance.Items.Single(i => i.Id == item.Id));
            }
            else
            {
                throw new GameException($"The game party must be at the same location as the item you are trying to pick up.\nParty location: {Party.Location}\nItem location: {item.Location}");
            }
        }


        public static Game Instance
        {
            get
            {
                instance ??= new Game();
                return instance;
            }
        }

        public static readonly IInteraction ExploreState = new Explore();

        public class Explore : IInteraction
        {
            public override string ToString()
            {
                return "Explore";
            }
        }
    }
}
