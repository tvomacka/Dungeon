# Dungeon
A simple top-down 2D adventure/RPG game made using a TDD approach.
Character/gameplay rules based on GURPS 4e.

## Player characters
### Attributes
|Attribute|Short|Description|Combat Effect|Dialogue Effect|Auxiliary Effect|
|---|---|---|---|---|---|
|Strength|STR|...|Melee Damage|Threats|Heavier weapons and armor|
|Dexterity|DEX|...|Chance to hit/dodge|...|...|
|Intelligence|INT|...|Spell Damage|Cleverness|More difficult spells|
|Vitality|VIT|...|Fatigue used for stronger attacks, health used to asbsorb damage|...|More life/fatigue points|
## NPCs

## Combat

## Inventory and Items

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
### Dialogue
Dialogue traverses through individual states, each state consists of a text and a list of options.
Each option can have a condition that determines if it is shown to the player (and therefore choosable by the player).
Each option can have actions attached to it, which will be executed before traversing to the next dialogue state.  
[Details](dialogue.md)
