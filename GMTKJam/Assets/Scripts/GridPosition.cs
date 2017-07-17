public class GridPosition
{
    public GridPosition(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public GridPosition()
    {
        this.X = 0;
        this.Y = 0;
    }

    public int X { get; set; }
    public int Y { get; set; }
}