public class SetPaintColor : IAST
{
    public SetPaintColor(Color color)
    {
        this.color = color;
        output = null;
    }

    public Color color { get; set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}