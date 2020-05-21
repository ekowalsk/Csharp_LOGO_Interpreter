using System.Collections.Generic;
public class ProcCall : IAST
{
    public string procName { get; private set; }
    public List <IAST> arguments { get; private set; }
    public ProcDecl procedure { get; set; }
    public ProcCall(string procName, List<IAST> args)
    {
        this.procName = procName;
        if (args == null)
            this.arguments = new List<IAST>();
        else
            this.arguments = args;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}
