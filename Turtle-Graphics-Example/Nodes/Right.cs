public class Right : IAST
{
    public Right(IAST exp)
    {
        expression = exp;
        output = null;
    }
    public IAST expression { get; private set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}