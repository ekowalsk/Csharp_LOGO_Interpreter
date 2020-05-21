using System.Text;
using System;

public class Lexer
{
    public Lexer(ISource isource)
    {
        source = isource;
        consume();
    }
    public Token token { get; set; }
    public void consume()
    {
        try
        {
            buildToken();
        }
        catch (Exception e)
        {
            type = Dictionary.LexemeType.etx;
            throw e;
        }
        token = createToken();
    }
    private void buildToken()
    {
        try
        {
            createNewLexemeBuffer();
            skipWhiteSpaceAndComments();
            setPosition();
            buildLexeme();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    private void createNewLexemeBuffer()
    {
        lexeme = new StringBuilder();
    }
    private void buildLexeme()
    {
        try
        {
            if (char.IsLetter(source.currentChar))
            {
                getIdentOrKeyWord();
            }
            else if (char.IsDigit(source.currentChar))
                getNumber();
            else if (source.currentChar == '\"')
                getString();
            else
                getSpecialSymbols();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    private Token createToken()
    {
        return new Token(type, lexeme.ToString(), new Position(startRow, startColumn));
    }
    private void skipWhiteSpaceAndComments()
    {
        while (char.IsWhiteSpace(source.currentChar) || source.currentChar == ';')
        {
            if (source.currentChar == ';')
                skipComments();
            else
                source.consume();
        }
    }
    private void skipComments()
    {
        if (source.currentChar == ';')
        {
            while (source.currentChar != '\n')
                source.consume();
            source.consume();
        }
    }
    private void setPosition()
    {
        startRow = source.row;
        startColumn = source.column;
    }
    private void getIdentOrKeyWord()
    {
        while (char.IsLetterOrDigit(source.currentChar) || source.currentChar == '_')
        {
            if (lexeme.Length > Dictionary.MAX_IDENT_LEN)
                throw new Exception(Dictionary.ident_size_exc + pos());
            lexeme.Append(source.currentChar);
            source.consume();
        }
        setIdentOrKeyWordType();
    }
    private void setIdentOrKeyWordType()
    {
        type = Dictionary.getType(lexeme.ToString());
        if (type == Dictionary.LexemeType.undefined)
            type = Dictionary.LexemeType.ident;
    }
    private void getNumber()
    {
        if (source.currentChar == '0')
        {
            lexeme.Append(source.currentChar);
            source.consume();
            if (source.currentChar == '0')
                throw new Exception(Dictionary.bad_number + pos());
        }
        getDigits();
        if (source.currentChar == ',')
        {
            lexeme.Append(source.currentChar);
            source.consume();
            if (!char.IsDigit(source.currentChar))
                throw new Exception(Dictionary.digit_exp + pos());
            getDigits();
        }
        type = Dictionary.LexemeType.number;
    }
    private string pos()
    {
        return "(" + startRow + "," + startColumn + ")";
    }
    private void getDigits()
    {
        while (char.IsDigit(source.currentChar))
        {
            lexeme.Append(source.currentChar);
            source.consume();
        }
    }
    private void getString()
    {
        source.consume();
        while (source.currentChar != '\"')
        {
            if (lexeme.Length > Dictionary.MAX_STRING_LEN)
                throw new Exception(Dictionary.string_size_exc + pos());
            if (source.currentChar == '\\')
            {
                escapeChar();
            }
            else
            {
                lexeme.Append(source.currentChar);
                source.consume();
            }
        }
        source.consume();
        type = Dictionary.LexemeType.strings;
    }
    private void escapeChar()
    {
        source.consume();
        lexeme.Append(source.currentChar);
        source.consume();
    }
    private void getSpecialSymbols()
    {
        if (tryDoubleSymbol())
        {
            lexeme.Append(source.currentChar);
            source.consume();
        }
        else trySingleSymbol();
    }
    private bool tryDoubleSymbol()
    {
        lexeme.Append(source.currentChar);
        source.consume();
        StringBuilder doubleSymbol = new StringBuilder(lexeme.ToString() + source.currentChar);
        type = Dictionary.getType(doubleSymbol.ToString());
        return (type == Dictionary.LexemeType.undefined) ? false : true;
    }
    private bool trySingleSymbol()
    {
        type = Dictionary.getType(lexeme.ToString());
        return (type == Dictionary.LexemeType.undefined) ? false : true;
    }
    private ISource source;
    private StringBuilder lexeme;
    private int startRow;
    private int startColumn;
    private Dictionary.LexemeType type;
}