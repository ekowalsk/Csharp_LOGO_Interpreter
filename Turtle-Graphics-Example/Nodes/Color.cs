using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Color : IAST
{
    public string name { get; private set; }
    public dynamic output { get; set; }
    public Color (string name)
    {
        this.name = name;
        this.output = null;
    }
    public void accept(IVisitor v)
    {
        v.visit(this);
    }
}
