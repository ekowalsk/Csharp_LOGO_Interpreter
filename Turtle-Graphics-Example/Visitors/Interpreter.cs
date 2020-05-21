#define UNCOMMENT
//#undef UNCOMMENT
using Nakov.TurtleGraphics;
using System;
using System.Collections.Generic;

public class Interpreter : IVisitor
{
    private Parser parser;
    private Stack stack;
    public Interpreter(Parser p)
    {
        parser = p;
        if (stack == null)
            stack = new Stack();
    }
    
    public void visit(AssignOp assignOp)
    {
        string name = assignOp.left.name;
        assignOp.right.accept(this);
        stack.pushGlobal(name, assignOp.right.output);
    }
    public void visit(BackWard backWard)
    {
        backWard.expression.accept(this);
#if (UNCOMMENT)
        Turtle.Forward(-backWard.expression.output);
#endif
    }
    public void visit(BinOp binOp)
    {
        binOp.left.accept(this);
        binOp.right.accept(this);
        switch (binOp.op)
        {
            case "+":
                binOp.output = binOp.left.output + binOp.right.output;
                break;
            case "-":
                binOp.output = binOp.left.output - binOp.right.output;
                break;
            case "*":
                binOp.output = binOp.left.output * binOp.right.output;
                break;
            case "/":
                binOp.output = binOp.left.output / binOp.right.output;
                break;
            case "<":
                binOp.output = binOp.left.output < binOp.right.output;
                break;
            case ">":
                binOp.output = binOp.left.output > binOp.right.output;
                break;
            case ">=":
                binOp.output = binOp.left.output >= binOp.right.output;
                break;
            case "<=":
                binOp.output = binOp.left.output >= binOp.right.output;
                break;
            case "<>":
                binOp.output = binOp.left.output != binOp.right.output;
                break;
            case "=":
                binOp.output = binOp.left.output == binOp.right.output;
                break;
            default:
                throw new Exception(" wrong operator: " + binOp.op);
        }
    }
    public void visit(Clean clean)
    {
#if (UNCOMMENT)
        Turtle.Reset();
#endif
    }
    public void visit(Compound compound)
    {
        foreach (IAST child in compound.children)
            child.accept(this);
    }
    public void visit(Color color)
    {
        switch (color.name)
        {
            case "czerwony":
                color.output = System.Drawing.Color.Red; 
                break;
            case "niebieski":
                color.output = System.Drawing.Color.Blue;
                break;
            default:
                throw new Exception("Nie ma takiego koloru: " + color.name);
        }
    }
    public void visit(Down down)
    {
#if (UNCOMMENT)
        Turtle.PenVisible = true;
#endif
    }
    public void visit(Forward forward)
    {
        forward.expression.accept(this);
#if (UNCOMMENT)
        Turtle.Forward(forward.expression.output);
#endif
    }
    public void visit(Hide hide)
    {
#if (UNCOMMENT)
        Turtle.ShowTurtle = false;
#endif
    }
    public void visit(IfElse ifElse)
    {
        ifElse.expression.accept(this);
        if (ifElse.expression.output == true)
            ifElse.block1.accept(this);
        else
            if (ifElse.block2 != null)
            ifElse.block2.accept(this);
    }
    public void visit(Left left)
    {
        left.expression.accept(this);
#if (UNCOMMENT)
        Turtle.Rotate(-left.expression.output);
#endif
    }
    public void visit(Num num)
    {
        float number;
        if (float.TryParse(num.value, out number))
            num.output = number;
        else
            throw new Exception("cannot parse number");
    }
    public void visit(Param param) { }
    public void visit(Print print)
    {
        string msg = "";
        foreach(IAST child in print.message)
        {
            child.accept(this);
            msg += child.output.toString() + " ";
        }
        
    }
    public void visit(ProcDecl procDecl)
    {
        stack.pushProcedure(procDecl);
    }
    public void visit(ProcCall procCall)
    {
        if (stack.level >= Dictionary.MAX_NEST_LEVEL)
            throw new Exception(Dictionary.max_nest_level_exc);
        if (!stack.containsProcedure(procCall.procName))
            throw new Exception("Don't know how to " + procCall.procName);
        ProcDecl returnedProcedure = stack.getProcedure(procCall.procName);
        if (procCall.arguments.Count != returnedProcedure.parameters.Count)
            throw new Exception("Arguments number mismatch in " + procCall.procName);
        stack.levelUp();
        pushParams(returnedProcedure, procCall);
        procCall.output = returnedProcedure;
        foreach (IAST child in procCall.output.children)
            child.accept(this);
        stack.clearParams();
        stack.levelDown();
    }
    private void pushParams(ProcDecl declaration, ProcCall call)
    {
        for (int i = 0; i < declaration.parameters.Count; i++)
        {
            call.arguments[i].accept(this);
            stack.pushLocal(declaration.parameters[i].varNode.name, call.arguments[i].output);
        }
    }
    public void visit(Program program)
    {
        foreach (IAST child in program.children)
            child.accept(this);
    }
    public void visit(Repeat repeat)
    {
        repeat.expression.accept(this);
        if (repeat.expression.output < 0)
            throw new Exception("wrong expression in repeat: ");
        int value = (int)Math.Round(repeat.expression.output);
        for (int i = 0; i < repeat.expression.output; i++)
            repeat.block.accept(this);
    }
    public void visit(Right right)
    {
        right.expression.accept(this);
#if (UNCOMMENT)
        Turtle.Rotate(right.expression.output);
#endif
    }
    public void visit(SetPaintColor setPaintColor)
    {
        
    }
    public void visit(SetPenColor setPenColor)
    {
        setPenColor.color.accept(this);
#if (UNCOMMENT)
        Turtle.PenColor = setPenColor.color.output;
#endif
    }
    public void visit(SetPos setPos)
    {
        setPos.expression1.accept(this);
        setPos.expression2.accept(this);
#if (UNCOMMENT)
        Turtle.MoveTo(setPos.expression1.output, setPos.expression2.output);
#endif
    }
    public void visit(Show show)
    {
#if (UNCOMMENT)
        Turtle.ShowTurtle = true;
#endif
    }
    public void visit(Strings strings)
    {
        strings.output = strings.value;
    }
    public void visit(Type type) { }
    public void visit(UnaryOp unaryOp)
    {
        string o = unaryOp.op;
        unaryOp.expression.accept(this);
        if (o == "-")
            unaryOp.output = -unaryOp.expression.output;
        else
            unaryOp.output = unaryOp.expression.output;
    }
    public void visit(Up up)
    {
#if (UNCOMMENT)
        Turtle.PenVisible = false;
#endif
    }
    public void visit(Var var)
    {
        if (stack.containsLocalVar(var.name, stack.level))
            var.output = stack.getLocalVar(var.name, stack.level);
        else if (stack.level != 0 && stack.containsLocalVar(var.name, stack.level-1))
            var.output = stack.getLocalVar(var.name, stack.level-1);
        else if (stack.containsGlobalVar(var.name))
            var.output = stack.getGlobalVar(var.name);
        else throw new Exception(Dictionary.no_var_exc + var.name);
    }
    public void visit(Whiles whiles)
    {
        whiles.expression.accept(this);
        while(whiles.expression.output == true)
            whiles.block.accept(this);
    }
}