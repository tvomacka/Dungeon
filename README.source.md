# Dungeon

[![on-push-do-docs](https://github.com/tvomacka/Dungeon/actions/workflows/on-push-do-docs.yml/badge.svg)](https://github.com/tvomacka/Dungeon/actions/workflows/on-push-do-docs.yml)

Framework for a 2D adventure/RPG game mad made using TDD approach.
Character/gameplay rules based on GURPS 4e.

> Only the parts of the original rules that are actually useful for the game will be implemented!

- [ ] [Coding Guidelines](Docs/codingGuidelines.md)
- [ ] [Knowledge Base](Docs/knowledgeBase.md)

## Game Initialization and Loading

The instance of the game is a singleton, that can be accessed using the property:

snippet: GameInstance

This instance can be filled with custom game data using one of the GameLoader*** classes from the Services namespace.

snippet: GameLoaderJson
