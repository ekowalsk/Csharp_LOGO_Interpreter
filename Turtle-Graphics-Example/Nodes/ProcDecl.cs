using System.Collections.Generic;
public class ProcDecl : IAST
{
    public string name { get; private set; }
    public List<IAST> children;
    public List<Param> parameters;
    public ProcDecl(string n, List<IAST> cList, List<Param> pList)
    {
        name = n;
        if (cList == null)
            children = new List<IAST>();
        else
            children = cList;
        if (pList == null)
            parameters = new List<Param>();
        else
            parameters = pList;
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}