using System.Collections.Generic;
using System;
public class Program : IAST
{
    public Program()
    {
        children = new List<IAST>();
        output = null;
    }
    public List<IAST> children;
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
    public dynamic output { get; set; }
}