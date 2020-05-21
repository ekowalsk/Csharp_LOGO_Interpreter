using System;
public class BinOp : IAST
{
    public string op { get; private set; }
    public IAST left { get; private set; }
    public IAST right { get; private set; }
    public BinOp(IAST l, Token t, IAST r)
    {
        left = l;
        op = t.getValue();
        right = r;
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}