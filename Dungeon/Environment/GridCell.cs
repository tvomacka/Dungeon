namespace Dungeon.Environment;

public class GridCell : ILocation
{
    public bool Traversable { get; set; }

    public bool IsTraversable()
    {
        return Traversable;
    }
}
