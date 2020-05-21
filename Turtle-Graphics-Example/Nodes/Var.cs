public class Var : IAST
{
    public Var(string name)
    {
        this.name = name;
        output = null;
    }
    public string name { get; private set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}