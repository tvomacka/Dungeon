﻿namespace Dungeon.GameLogic;

public class Quest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Quest {Id}: {Name}";
    }
}