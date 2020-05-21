public class Param : IAST
{
    public Var varNode { get; private set; }
    public Type typeNode { get; private set; }
    public Param(Var v, Type t)
    {
        varNode = v;
        typeNode = t;
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}