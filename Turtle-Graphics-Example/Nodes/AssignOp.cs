public class AssignOp : IAST
{
    public Var left { get; private set; }
    public Type type { get; private set; }
    public IAST right { get; private set; }
    public AssignOp(Var l, Type t, IAST r)
    {
        left = l;
        type = t;
        right = r;
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}