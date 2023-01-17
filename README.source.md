# Dungeon

[![on-push-do-docs](https://github.com/tvomacka/Dungeon/actions/workflows/on-push-do-docs.yml/badge.svg)](https://github.com/tvomacka/Dungeon/actions/workflows/on-push-do-docs.yml)
[![Test, Release and Deploy to NuGet](https://github.com/tvomacka/Dungeon/actions/workflows/on-version-test-release-depploy.yml/badge.svg)](https://github.com/tvomacka/Dungeon/actions/workflows/on-version-test-release-depploy.yml)

Framework for a 2D adventure/RPG game made using TDD approach.
Character/gameplay rules based on GURPS 4e.

> Only the parts of the original rules that are actually useful for the game will be implemented!

- [ ] [Coding Guidelines](Docs/codingGuidelines.md)
- [ ] [Knowledge Base](Docs/knowledgeBase.md)

## Game Initialization and Loading

The instance of the game is a singleton, that can be accessed using the property:

snippet: GameInstance

This instance can be filled with custom game data using one of the GameLoader*** classes from the Services namespace.

snippet: GameLoaderJson

### Sample Games

There are some simple game setups stored in the [Game Samples Folder](DungeonTests/TestResources/Games) that demonstrate some of the basic concepts of the Dungeon Toolkit.

- [ ] [Dialogue Condition](DungeonTests/TestResources/Games/dialogueCondition.json) Contains a simple dialog with three possible answers, one of which is disabled because the *John Smith* character's intelligence is not greater than the requiered value.
- [ ] [Fetch Quest](DungeonTests/TestResources/Games/fetchQuest.json) Is a game instance in which the party has a quest to bring an item to a NPC. After this item is picked up in the inventory, the NPC dialogue will enable an option to give this item to the NPC and therefore complete the quest. After choosing this dialogue option, the quest is completed, the item is removed from character's inventory and experience points are assigned.
