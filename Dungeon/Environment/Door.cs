namespace Dungeon.Environment;

public class Door : ILocation
{
    private bool isOpen = true;

    public bool IsOpen()
    {
        return isOpen;
    }

    public Door(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    public bool IsTraversable()
    {
        return IsOpen();
    }

    public void Open()
    {
        isOpen = true;
    }

    public override string ToString()
    {
        return (isOpen ? "Open" : "Closed") + " door";
    }
}
