public class IfElse : IAST
{
    public IfElse(IAST exp, IAST b1, IAST b2)
    {
        expression = exp;
        block1 = b1;
        block2 = b2;
        output = null;
    }
    public IAST expression { get; private set; }
    public IAST block1 { get; private set; }
    public IAST block2 { get; private set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}