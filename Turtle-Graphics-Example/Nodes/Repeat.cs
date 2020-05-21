public class Repeat : IAST
{
    public Repeat(IAST exp, IAST b)
    {
        expression = exp;
        block = b;
        output = null;
    }
    public IAST expression { get; private set; }
    public IAST block { get; private set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}