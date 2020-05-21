public interface ISource
{
    char currentChar { get; }
    int row { get; set; }
    int column { get; set; }
    void consume();
}