public class Type : IAST
{
    public string name { get; private set; }
    public Type(Token t)
    {
        name = t.getValue();
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}