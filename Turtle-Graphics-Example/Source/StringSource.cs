public class StringSource : ISource
{
    public StringSource(string s)
    {
        stringSource = s;
        positionInString = 0;
        row = 1;
        column = 0;
        consume();
    }
    public char currentChar { get; set; }
    public int row { get; set; }
    public int column { get; set; }
    public void consume()
    {
        moveToNextChar();
        updatePosition();
    }
    private void updatePosition()
    {
        if (currentChar == '\n')
        {
            row++;
            column = 0;
        }
        else
            column++;
    }
    private void moveToNextChar()
    {
        if (positionInString < stringSource.Length)
        {
            currentChar = stringSource[positionInString];
            positionInString++;
        }
        else
            currentChar = Dictionary.ETX;
    }
    private string stringSource;
    private int positionInString;
}