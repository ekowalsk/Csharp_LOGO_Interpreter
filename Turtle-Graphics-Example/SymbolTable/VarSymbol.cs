using System;
public class VarSymbol : ISymbol
{
    public string name { get; private set; }
    public string type { get; private set; }
    public VarSymbol(string n, string t)
    {
        name = n;
        type = t;
    }
    public void print()
    {
        Console.Write(" " + (name, type) + " ");
    }
}