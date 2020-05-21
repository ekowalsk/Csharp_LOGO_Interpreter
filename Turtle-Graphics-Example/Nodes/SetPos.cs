public class SetPos : IAST
{
    public SetPos(IAST exp1, IAST exp2)
    {
        expression1 = exp1;
        expression2 = exp2;
        output = null;
    }
    public IAST expression1 { get; private set; }
    public IAST expression2 { get; private set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}