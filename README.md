# Dungeon

Framework for a 2D adventure/RPG game mad made using TDD approach.
Character/gameplay rules based on GURPS 4e.

> Only the parts of the original rules that are actually useful for the game will be implemented!

[Coding Guidelines](Docs/codingGuidelines.md)

## Theme

The idea is that the framework itself should be as general as possible, but since it is meant to be actually used for creating a game, we cannot make it too general. The game will have some specifics to it and the framework has to support them. There are many sources of inspiration for the game, mainly among games books and movies. Listing just a few of the games that have inspired me, there are such as classics as Fallout 1/2, Planescape: Torment, TES (mainly Daggerfall and Skyrim), Might and Magic, Dragon Age, Dungeon Master 2, and others.

- Fantasy Setting
- Open World
- Hex Grid
- Isometric 3D


## General Game Progression

1. [Character creation](Docs/characterCreation.md)
1. [Introduction to the story](Docs/introductionToTheStory.md)
1. Exploring the environment
    1. [Basic Interactions](Docs/basicInteractions.md)
    1. Combat
    1. [Dialogue](Docs/dialogue.md)
1. Acquiring first quests
1. Discovering new/advanced interactions
1. Shopping
1. Gaining XP
1. Gaining Levels
1. Using special skills
1. Puzzle/minigames/whatever
1. Gaining new party members
1. Map travelling

## Game mechanics

The game cycles through the following states:
|Game State|Description|
|---|---|
|Explore|The initial state of the game where the party moves around the map.|
|Dialogue|The party engages in dialogue with an NPC.|
|Combat|The party fights a group of monsters.|
|Shop|For buying and selling items from vendors.|
|Inventory|For inventory management and picking up items.|
|Character|For player character management.|

```mermaid
graph TD;
    Explore-->Dialogue;
    Explore-->Combat;
    Explore-->Shop;
    Explore-->Inventory;
    Explore-->Character;
    Dialogue-->Explore;
    Dialogue-->Combat;
    Combat-->Explore;
    Shop-->Explore;
    Inventory-->Explore;
    Character-->Explore;
```

### Character Progression

Characters get experience points (XP) for completing quests and proceeding through the game. This may include killing some NPCs/Monsters, but no XP is given for just killing opponents.
[Details](progression.md)

### Story Development

> Player choices need to have an impact on the environment (and obviously game progression).

The player needs to feel that the characters are living in a dynamic environment and that his choices throughout the game can and do impact the environment. This is achieved partially by the story being non-linear and partially by the engine allowing the environment to react to the player's actions.

## NPCs

## Combat

## Inventory and Items

Some of the items, such as weapons have a minimum ST requirement. In GURPS, this means the character without the required ST will receive a skill penalty using the weapon and spends fatigue at the end of each prolonged combat. It might be convenient to implement the minimum ST requirement in the more common way which prevents the character from wielding the weapon at all if they don't have the required amount of ST.

Unlike weapons, armor does not have a minimum ST requirement, but the heavier armor obviously encumbers the character more. And characters with lower amount of ST become encumbered easier.
