using System.Collections.Generic;
public class Print : IAST
{
    public Print(List<IAST> mess)
    {
        if (mess == null)
            message = new List<IAST>();
        else
            message = mess;
        output = null;
    }
    public List<IAST> message { get; private set; }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}