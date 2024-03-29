﻿using System.Drawing;

namespace Dungeon.GameLogic.Equipment;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Point Location { get; set; }

    public override string ToString()
    {
        return $"Item {Id}: {Name}";
    }

    public override bool Equals(object obj)
    {
        return (obj as Item)?.Id == Id;
    }
}