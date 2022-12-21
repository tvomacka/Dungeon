using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using Dungeon.GameLogic;
using Dungeon.GameLogic.Dialogues;
using Dungeon.GameLogic.Equipment;
using Dungeon.GameLogic.Exceptions;

namespace DungeonTests;

[UseReporter(typeof(VisualStudioReporter))]
[TestClass]
public class GameLogicTests
{
    private Game game = Game.Instance;

    public static void LoadTestGame(string gameName)
    {
        Game.Instance.Load(@"..\..\..\TestResources\Games\" + gameName);
    }


    [TestMethod]
    public void PartyLocation_CanBeLoadedFromFile()
    {
        LoadTestGame("test.json");
        Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());
    }

    [TestMethod]
    public void PartyCharacter_CanBeLoadedFromFile()
    {
        LoadTestGame("party.json");
        Assert.AreEqual(1, game.Party.Members.Count());
    }

    [TestMethod]
    public void LoadedPlayerCharacter_HasCorrectInt()
    {
        LoadTestGame("party.json");
        Assert.AreEqual(10, game.Party[0].Intelligence);
    }

    [TestMethod]
    public void Party_CanMoveTowardsNpc()
    {
        LoadTestGame("test.json");
        Assert.AreEqual("{X=3,Y=5}", game.Party.Location.ToString());

        var npc = game.GetCharacter("QuestNPC");
        game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);

        Assert.AreEqual("{X=4,Y=5}", game.Party.Location.ToString());
    }

    [TestMethod]
    public void Item_CanBeLoaded_FromJson()
    {
        LoadTestGame("fetchQuest.json");

        Assert.AreEqual(1, game.Items.Count);
        Assert.AreEqual("Item 0: Quest Item", game.Items[0].ToString());
    }

    [TestMethod]
    public void Quest_CanBeLoaded_FromJson()
    {
        LoadTestGame("fetchQuest.json");

        Assert.AreEqual(1, game.Quests.Count);
        Assert.AreEqual("Quest 0: Fetch Quest Test", game.Quests[0].ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(GameException))]
    public void Item_CannotBePickedUp_FromDistance()
    {
        LoadTestGame("fetchQuest.json");

        game.PickUpItem(0, 0);
    }

    [TestMethod]
    public void Item_CanBeEquiped_FromInventory()
    {
        var pc = new PlayerCharacter();
        var dagger = new Weapon() { Id = 0, Name = "Dagger"};
        pc.Inventory.Add(dagger);
        Assert.IsTrue(pc.RightHand.IsEmpty());
        pc.Equip(pc.Inventory[0], pc.RightHand);
        Assert.IsTrue(pc.RightHand.IsEquippedWith(dagger));
    }

    [TestMethod]
    [ExpectedException(typeof(GameException))]
    public void Item_CannotBeEquiped_ByACharacterWithoutRequiredStrength()
    {
        var pc = new PlayerCharacter() { Strength = 10 };
        var hammer = new Weapon() { Id = 1, Name = "Hammer", MinStrength = 12 };
        game.Items.Add(hammer);
        
        pc.Equip(hammer, pc.RightHand);
    }

    [TestMethod]
    public void Item_CanBePickedUp_FromTheGround()
    {
        LoadTestGame("fetchQuest.json");
        
        game.Party.MoveTo(game.Items[0].Location);
        game.PickUpItem(0, 0);

        Assert.IsTrue(game.Party[0].Inventory.Contains(game.Items[0]));
    }

    [TestMethod]
    public void FetchQuest_WithoutTheQuestItem_CannotFinish()
    {
        LoadTestGame("fetchQuest.json");
        var npc = game.GetCharacter("QuestNPC");
        game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        game.State = game.DialogueWith(npc);

        Assert.AreEqual(1, (game.State as Dialogue).GetFilteredOptions().Count());
    }

    [TestMethod]
    public void Character_CanGainXP()
    {
        LoadTestGame("fetchQuest.json");

        Assert.AreEqual(0, game.Party[0].XP);
        game.Party[0].AddXP(100);
        Assert.AreEqual(100, game.Party[0].XP);
    }

    [TestMethod]
    public void FetchQuest_Complete_Test()
    {
        LoadTestGame("fetchQuest.json");
        game.Party.MoveTo(game.Items[0].Location);
        game.PickUpItem(0, 0);

        var npc = game.GetCharacter("QuestNPC");
        game.Party.MoveTo(npc.Location.X - 1, npc.Location.Y);
        game.State = game.DialogueWith(npc);
        game.State = (game.State as Dialogue).ChooseOption(1);

        Assert.IsFalse(game.Party[0].Inventory.Contains(game.Items[0]));
        Assert.AreEqual(0, game.Party.ActiveQuests.Count());
        Assert.AreEqual(100, game.Party[0].XP);
    }

    [TestMethod]
    public void PlayerCharacter_HasStrengthAttribute()
    {
        var pc = new PlayerCharacter()
        {
            Strength = 20
        };

        Assert.AreEqual(20, pc.Strength);
    }

    [TestMethod]
    public void PlayerCharacter_HasDexterityAttribute()
    {
        var pc = new PlayerCharacter()
        {
            Dexterity = 20
        };

        Assert.AreEqual(20, pc.Dexterity);
    }

    [TestMethod]
    public void PlayerCharacter_HasHealthAttribute()
    {
        var pc = new PlayerCharacter()
        {
            Health = 20
        };

        Assert.AreEqual(20, pc.Health);
    }
}