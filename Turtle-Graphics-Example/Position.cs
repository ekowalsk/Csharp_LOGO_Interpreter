public class Position
{
    public Position(int r, int c)
    {
        row = r;
        column = c;
    }
    public (int, int) getPosition()
    {
        return (row, column);
    }
    private int row;
    private int column;
}