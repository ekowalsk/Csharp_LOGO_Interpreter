public class Up : IAST
{
    public Up()
    {
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}