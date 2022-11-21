﻿namespace Dungeon.GameLogic
{
    public class Item
    {
        public static readonly string InInventory = "InInventory";
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public Point Location { get; set; }

        public override string ToString()
        {
            return $"Item {Id}: {Name}";
        }
    }
}