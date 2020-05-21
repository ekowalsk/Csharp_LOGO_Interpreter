public class UnaryOp : IAST
{
    public string op { get; private set; }
    public dynamic expression { get; private set; }
    public UnaryOp(Token t, dynamic exp)
    {
        op = t.getValue();
        expression = exp;
        output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}