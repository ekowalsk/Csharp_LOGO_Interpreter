public class Down : IAST
{
    public Down()
    {
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}