using System;
using System.Collections.Generic;

public class Stack
{
    private Dictionary<string, dynamic>[] localVariables;
    private static Dictionary<string, dynamic> globalVariables;
    private static Dictionary<string, ProcDecl> procedures;
    public int level { get; private set; }

    public Stack()
    {
        initLocalVariables();
        if (globalVariables == null)
            globalVariables = new Dictionary<string, dynamic>();
        if (procedures == null)
            procedures = new Dictionary<string, ProcDecl>();
        level = 0;
    }
    public bool containsProcedure(string name)
    {
        return procedures.ContainsKey(name);
    }
    public bool containsLocalVar(string name, int lvl)
    {
        return localVariables[lvl].ContainsKey(name);
    }
    public ProcDecl getProcedure(string name)
    {
        ProcDecl retProc;
        if (procedures.TryGetValue(name, out retProc))
            return retProc;
        else
            throw new Exception("procedure not found: " + name);
    }
    public bool containsGlobalVar(string name)
    {
        return globalVariables.ContainsKey(name);
    }
    public void pushLocal(string name, dynamic value)
    {
        localVariables[level][name] = value;
    }
    public void pushProcedure(ProcDecl procedure)
    {
        procedures[procedure.name] = procedure;
    }
    public void pushGlobal(string name, dynamic value)
    {
        globalVariables[name] = value;
    }
    public void levelUp()
    {
        level++;
    }
    public void levelDown()
    {
        level--;
    }
    public dynamic getLocalVar(string name, int lvl)
    {
        dynamic value;
        if (localVariables[lvl].TryGetValue(name, out value))
            return value;
        
        else throw new Exception("cant get value: " + name);
    }
    public dynamic getGlobalVar(string name)
    {
        dynamic value;
        if (globalVariables.TryGetValue(name, out value))
            return value;
        else throw new Exception("cant get value: " + name);
    }
    private void initLocalVariables()
    {
        localVariables = new Dictionary<string, dynamic>[Dictionary.MAX_NEST_LEVEL];
        for (int i = 0; i < Dictionary.MAX_NEST_LEVEL; i++)
        {
            localVariables[i] = new Dictionary<string, dynamic>();
        }
    }
    public void clearParams()
    {
        localVariables[level] = new Dictionary<string, dynamic>();
    }


}