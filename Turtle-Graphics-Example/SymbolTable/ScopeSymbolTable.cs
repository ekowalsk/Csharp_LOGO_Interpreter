using System.Collections.Generic;
using System;
public class ScopeSymbolTable
{
    public Dictionary<string, ISymbol> symbols;
    public ScopeSymbolTable(int level, ScopeSymbolTable enclosingScope)
    {
        symbols = new Dictionary<string, ISymbol>();
        this.scopeLevel = level;
        this.globalScope = enclosingScope;
        init();
    }
    public int scopeLevel { get; private set; }
    public ScopeSymbolTable globalScope { get; private set; }
    public void define(ISymbol symbol)
    {
        symbols[symbol.name] = symbol;
    }
    public ISymbol find(string name)
    {
        ISymbol value;
        if (symbols.TryGetValue(name, out value))
            return value;
        else if (globalScope != null && globalScope.symbols.TryGetValue(name, out value))
            return value;
        else
            throw new Exception("symbol not found: " + name);
    }
    public bool contains(string name)
    {
        return symbols.ContainsKey(name);
    }

    private void init()
    {
        define(new BuiltInTypeSymbol("integer"));
        define(new BuiltInTypeSymbol("num"));
    }
}