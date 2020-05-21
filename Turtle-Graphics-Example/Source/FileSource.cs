using System.IO;
public class FileSource : ISource
{
    public char currentChar { get; set; }
    public void consume()
    {
        currentChar = readChar();
        updatePosition();
    }
    public FileSource(string fileName)
    {
        streamReader = new StreamReader(fileName, System.Text.Encoding.UTF8);
        row = 1;
        column = 1;
        currentChar = readChar();
    }
    private StreamReader streamReader;
    public int column { get; set; }
    public int row { get; set; }
    private char readChar()
    {
        char[] buf = new char[1];
        streamReader.Read(buf, 0, 1);
        return buf[0];
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
}