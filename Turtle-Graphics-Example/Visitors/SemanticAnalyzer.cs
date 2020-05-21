using System;
using System.Collections.Generic;
public class SemanticAnalyzer : IVisitor
{
    public static ScopeSymbolTable scope;
    public SemanticAnalyzer()
    {
        if (scope == null)
            scope = new ScopeSymbolTable(0, null);
    }
    public void visit(AssignOp assignOp)
    {
        if(!scope.contains(assignOp.left.name))
            declareSymbol(assignOp.left, assignOp.type);
        assignOp.right.accept(this);
    }
    public void visit(BackWard backWard)
    {
        backWard.expression.accept(this);
    }
    public void visit(BinOp binOp)
    {
        binOp.left.accept(this);
        binOp.right.accept(this);
    }
    public void visit(Clean clean) { }
    public void visit(Compound compound)
    {
        foreach (IAST child in compound.children)
            child.accept(this);
    }
    public void visit(Color color) { }
    public void visit(Down down) { }
    public void visit(Forward forward)
    {
        forward.expression.accept(this);
    }
    public void visit(Hide hide) { }
    public void visit(IfElse ifElse)
    {
        ifElse.expression.accept(this);
        ifElse.block1.accept(this);
        if (ifElse.block2 != null)
            ifElse.block2.accept(this);
    }
    public void visit(Left left)
    {
        left.expression.accept(this);
    }
    public void visit(Num num) { }
    public void visit(Param param) { }
    public void visit(ProcCall procCall) { }
    public void visit(ProcDecl procDecl)
    {
        if (scope.scopeLevel == Dictionary.MAX_NEST_LEVEL)
            throw new Exception(Dictionary.max_nest_level_exc);
        ProcedureSymbol procSym = new ProcedureSymbol(procDecl.name);
        scope.define(procSym);
        scope = new ScopeSymbolTable(scope.scopeLevel + 1, scope);
        foreach (Param param in procDecl.parameters)
        {
            VarSymbol paramSymbol = createParamSymbol(param);
            scope.define(paramSymbol);
            procSym.parameters.Add(paramSymbol);
        }
        foreach (IAST child in procDecl.children)
            child.accept(this);
        scope = scope.globalScope;
    }
    private VarSymbol createParamSymbol(Param p)
    {
        string paramType = scope.find(p.typeNode.name).name;
        string paramName = p.varNode.name;
        return new VarSymbol(paramName, paramType);
    }
    public void visit (Print print) { }
    public void visit(Program program)
    {
        foreach (IAST child in program.children)
            child.accept(this);
    }
    public void visit(Repeat repeat)
    {
        repeat.expression.accept(this);
        repeat.block.accept(this);
    }
    public void visit(Right right)
    {
        right.expression.accept(this);
    }
    public void visit(SetPaintColor setPaintColor) { }
    public void visit(SetPenColor setPenColor) { }
    public void visit(SetPos setPos)
    {
        setPos.expression1.accept(this);
        setPos.expression2.accept(this);
    }
    public void visit(Show show) { }
    public void visit(Strings strings) { }
    public void visit(Type type) { }
    public void visit(UnaryOp unaryOp)
    {
        visit(unaryOp.expression);
    }
    public void visit(Up up) { }

    public void visit(Var var)
    {
        string varName = var.name;
        ISymbol s = scope.find(varName);
    }
    public void visit(Whiles whiles)
    {
        whiles.expression.accept(this);
        whiles.block.accept(this);
    }
    private void declareSymbol(Var node, Type t)
    {
        ISymbol typeSymbol = scope.find(t.name);
        string varName = node.name;
        VarSymbol varSymbol = new VarSymbol(varName, typeSymbol.name);
        scope.define(varSymbol as ISymbol);
    }
}