public class BuiltInTypeSymbol : ISymbol
{
    public string name { get; private set; }
    public BuiltInTypeSymbol(string n)
    {
        name = n;
    }
}