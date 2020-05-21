using System;
using System.Collections.Generic;
public class Parser
{
    public Parser(Lexer lex)
    {
        lexer = lex;
    }
    public IAST program()
    {
        Program program = new Program();
        while (tokenType() != Dictionary.LexemeType.etx)
            program.children.Add(statement());
        return program;
    }
    private IAST statement()
    {
        switch (tokenType())
        {
            case Dictionary.LexemeType.hide:
                return hide();
            case Dictionary.LexemeType.show:
                return show();
            case Dictionary.LexemeType.up:
                return up();
            case Dictionary.LexemeType.down:
                return down();
            case Dictionary.LexemeType.clean:
                return clean();
            case Dictionary.LexemeType.forward:
                return forward();
            case Dictionary.LexemeType.backward:
                return backWard();
            case Dictionary.LexemeType.left:
                return left();
            case Dictionary.LexemeType.right:
                return right();
            case Dictionary.LexemeType.setpos:
                return setPos();
            case Dictionary.LexemeType.repeat:
                return repeat();
            case Dictionary.LexemeType.whiles:
                return whiles();
            case Dictionary.LexemeType.setPaintColor:
                return setPaintColor();
            case Dictionary.LexemeType.setPenColor:
                return setPenColor();
            case Dictionary.LexemeType.make:
                return assignOp();
            case Dictionary.LexemeType.ifelse:
                return ifelse();
            case Dictionary.LexemeType.begin:
                return procedure();
            case Dictionary.LexemeType.ident:
                return procCall();
            default:
                throw new Exception(Dictionary.instruction_exp + pos());
        }
    }

    private Hide hide()
    {
        lexer.consume();
        return new Hide();
    }
    private Show show()
    {
        lexer.consume();
        return new Show();
    }
    private Up up()
    {
        lexer.consume();
        return new Up();
    }
    private Down down()
    {
        lexer.consume();
        return new Down();
    }
    private Clean clean()
    {
        lexer.consume();
        return new Clean();
    }
    private Left left()
    {
        lexer.consume();
        IAST exp = expression();
        return new Left(exp);
    }
    private Right right()
    {
        lexer.consume();
        IAST exp = expression();
        return new Right(exp);
    }
    private BackWard backWard()
    {
        lexer.consume();
        IAST exp = expression();
        return new BackWard(exp);
    }
    private Forward forward()
    {
        lexer.consume();
        IAST exp = expression();
        return new Forward(exp);
    }
    private Whiles whiles()
    {
        lexer.consume();
        return new Whiles(expression(), squarelBlock());
    }
    private Repeat repeat()
    {
        lexer.consume();
        return new Repeat(expression(), squarelBlock());
    }
    private SetPenColor setPenColor()
    {
        lexer.consume();
        Color c = color();
        return new SetPenColor(c);
    }
    private SetPaintColor setPaintColor()
    {
        lexer.consume();
        Color c = color();
        return new SetPaintColor(c);
    }
    private SetPos setPos()
    {
        lexer.consume();
        IAST exp1 = expression();
        IAST exp2 = expression();
        return new SetPos(exp1, exp2);
    }
    private IAST procedure()
    {
        lexer.consume();
        Token ident = lexer.token;
        List<Param> p = new List<Param>();
        if (consumeToken(Dictionary.LexemeType.ident, Dictionary.ident_exp))
        {
            if (consumeToken(Dictionary.LexemeType.lbracket, Dictionary.lbracket_exp))
            {
                if (tokenType() == Dictionary.LexemeType.ident)
                    readParams(p);
                if (consumeToken(Dictionary.LexemeType.rbracket, Dictionary.rbracket_exp))
                {
                    return new ProcDecl(ident.getValue(), procBlock(), p);
                }
            }
        }
        return null;
    }
    private ProcCall procCall()
    {
        string name = lexer.token.getValue();
        lexer.consume();
        List<IAST> arguments = new List<IAST>();
        readArguments(arguments);
        return new ProcCall(name, arguments);
    }
    private void readArguments(List<IAST> args)
    {
        if (consumeToken(Dictionary.LexemeType.lbracket, Dictionary.lbracket_exp))
        {
            if (lexer.token.getType() == Dictionary.LexemeType.rbracket)
            {
                lexer.consume();
                return;
            }
            while (lexer.token.getType() != Dictionary.LexemeType.rbracket)
            {
                IAST argument = expression();
                args.Add(argument);
            }
            consumeToken(Dictionary.LexemeType.rbracket, Dictionary.rbracket_exp);
        }
    }
    private void readParams(List<Param> p)
    {
        Token ident = lexer.token;
        if (consumeToken(Dictionary.LexemeType.ident, Dictionary.ident_exp))
        {
            Token type = lexer.token;
            if (consumeToken(Dictionary.LexemeType.type, Dictionary.type_exp))
            {
                p.Add(new Param(new Var(ident.getValue()), new Type(type)));
                if (tokenType() == Dictionary.LexemeType.separator)
                {
                    lexer.consume();
                    readParams(p);
                }
            }
        }
    }
    private IAST factor()
    {
        switch (tokenType())
        {
            case Dictionary.LexemeType.sub:
            case Dictionary.LexemeType.add:
                Token t = lexer.token;
                lexer.consume();
                UnaryOp node0 = new UnaryOp(t, factor());
                return node0;
            case Dictionary.LexemeType.number:
                Num node1 = new Num(lexer.token);
                lexer.consume();
                return node1;
            case Dictionary.LexemeType.ident:
                Var node2 = new Var(lexer.token.getValue());
                lexer.consume();
                return node2;
            case Dictionary.LexemeType.lbracket:
                consumeToken(Dictionary.LexemeType.lbracket, Dictionary.lbracket_exp);
                dynamic node3 = expression();
                consumeToken(Dictionary.LexemeType.rbracket, Dictionary.rbracket_exp);
                return node3;
            default:
                throw new Exception(Dictionary.factor_exp + pos());
        }
    }
    private IAST term()
    {
        IAST node = factor();
        while (tokenType() == Dictionary.LexemeType.mul
              || tokenType() == Dictionary.LexemeType.div)
        {
            Token t = lexer.token;
            lexer.consume();
            node = new BinOp(node, t, factor());
        }
        return node;
    }
    private IAST expression()
    {
        IAST node = simpExpression();
        if (tokenType() == Dictionary.LexemeType.relop)
        {
            Token t = lexer.token;
            lexer.consume();
            node = new BinOp(node, t, simpExpression());
        }
        return node;
    }
    private IAST simpExpression()
    {
        IAST node = term();
        while (tokenType() == Dictionary.LexemeType.add
              || tokenType() == Dictionary.LexemeType.sub)
        {
            Token t = lexer.token;
            lexer.consume();
            node = new BinOp(node, t, term());
        }
        return node;
    }
    private AssignOp assignOp()
    {
        lexer.consume();
        Token type = lexer.token;
        consumeToken(Dictionary.LexemeType.type, Dictionary.type_exp);
        Token ident = lexer.token;
        consumeToken(Dictionary.LexemeType.ident, Dictionary.ident_exp);
        consumeToken(Dictionary.LexemeType.assignop, Dictionary.assignop_exp);
        return new AssignOp(new Var(ident.getValue()), new Type(type), expression());
    }
    private IAST ifelse()
    {
        lexer.consume();
        IAST exp = expression();
        IAST block1 = squarelBlock();
        IAST block2 = null;
        if (tokenType() == Dictionary.LexemeType.squarelbracket)
            block2 = squarelBlock();
        return new IfElse(exp, block1, block2);
    }
    private Compound squarelBlock()
    {
        consumeToken(Dictionary.LexemeType.squarelbracket, Dictionary.squarel_exp);
        Compound compound = block();
        consumeToken(Dictionary.LexemeType.squarerbracket, Dictionary.squarer_exp);
        return compound;
    }
    private Color color()
    {
        Token color = lexer.token;
        lexer.consume();
        return new Color(color.getValue());
    }
    private Compound block()
    {
        Compound compound = new Compound();
        while (tokenType() != Dictionary.LexemeType.squarerbracket)
            compound.children.Add(statement());
        return compound;
    }
    private List<IAST> procBlock()
    {
        List<IAST> block = new List<IAST>();
        while (tokenType() != Dictionary.LexemeType.end)
            block.Add(statement());
        consumeToken(Dictionary.LexemeType.end, Dictionary.end_expected);
        return block;
    }
    private bool consumeToken(Dictionary.LexemeType type, string exceptionMessage)
    {
        if (tokenType() == type)
        {
            lexer.consume();
            return true;
        }
        throw new Exception(exceptionMessage + pos());
    }
    private (int, int) pos()
    {
        return lexer.token.getPosition();
    }
    private Dictionary.LexemeType tokenType()
    {
        return lexer.token.getType();
    }
    private Lexer lexer;
}