using System.Collections.Generic;
public class ProcedureSymbol : ISymbol
{
    public string name { get; private set; }
    public List<VarSymbol> parameters { get; private set; }
    public ProcedureSymbol(string n, List<VarSymbol> p = null)
    {
        name = n;
        if (p == null)
            parameters = new List<VarSymbol>();
        else
            parameters = p;
    }
}