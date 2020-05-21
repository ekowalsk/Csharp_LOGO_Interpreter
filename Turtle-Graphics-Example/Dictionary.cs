using System.Collections.Generic;
public class Dictionary
{
    public enum LexemeType
    {
        add, sub, assign, assignop, begin, colon, color, end, etx, ident, ifelse, invalid, lbracket,
        make, mul, div, number, rbracket, relop, separator, squarelbracket, squarerbracket,
        strings, undefined, underscore, setpos,
        type, compound,
        write, read,
        forward, backward, left, right,
        hide, show, up, down, clean,
        setPaintColor, setPenColor,
        repeat, whiles
    }
    public static Dictionary<string, LexemeType> keyWords = new Dictionary<string, LexemeType>{
        {"oto", LexemeType.begin}, {"już", LexemeType.end}, {"(", LexemeType.lbracket}, {")", LexemeType.rbracket},
        {"+", LexemeType.add}, {"-", LexemeType.sub}, {"or", LexemeType.add}, {"*", LexemeType.mul},
        {"/", LexemeType.div}, {"and", LexemeType.mul}, {":=", LexemeType.assignop}, {"niech", LexemeType.make},
        {"przyp", LexemeType.assign}, {"np", LexemeType.forward}, {"ws", LexemeType.backward}, {"lw", LexemeType.left},
        {"pw", LexemeType.right}, {"sż", LexemeType.hide}, {"pż", LexemeType.show},
        {"ustawpoz", LexemeType.setpos}, {"ukp", LexemeType.setPenColor}, {"ukm", LexemeType.setPaintColor},
        {"pod", LexemeType.up}, {"opu", LexemeType.down},
        {"pisz", LexemeType.write}, {"czytaj", LexemeType.read}, {"kolor", LexemeType.color},
        {"[", LexemeType.squarelbracket}, {"]", LexemeType.squarerbracket}, {"dopóki", LexemeType.whiles},
        {"powtórz", LexemeType.repeat}, {"jeślitaknie", LexemeType.ifelse}, {";", LexemeType.colon},
        {"_", LexemeType.underscore}, {"=", LexemeType.relop}, {"<", LexemeType.relop}, {">", LexemeType.relop},
        {"<>", LexemeType.relop}, {"<=",LexemeType.relop}, {">=", LexemeType.relop},
        {",", LexemeType.separator}, {"\0", LexemeType.etx}, {"num", LexemeType.type},
        {"czyść", LexemeType.clean}
};
    public static LexemeType getType(string symbol)
    {
        LexemeType type;
        return keyWords.TryGetValue(symbol, out type) ? type : LexemeType.undefined;
    }
    public const string ident_exp = " identifier expected: ";
    public const string assignop_exp = " ':=' expected: ";
    public const string type_exp = " type expected: ";
    public const string lbracket_exp = " left bracket expected: ";
    public const string rbracket_exp = " right bracket expected: ";
    public const string factor_exp = " number, identifier or left bracket expected: ";
    public const string squarel_exp = " '[' expected: ";
    public const string squarer_exp = " ']' expected: ";
    public const string instruction_exp = " instruction expected: ";
    public const string separator_exp = " separator expected: ";
    public const string digit_exp = " digit expected: ";
    public const string bad_number = " bad number: ";
    public const string ident_size_exc = " maximum identifier size exceed: ";
    public const string string_size_exc = " maximum string size exceeded: ";
    public const string no_var_exc = " variable not found: ";
    public const string end_expected = " już keyword expected: ";
    public const string max_nest_level_exc = " maximal nest level exceeded ";

    public const char ETX = '\0';
    public const int MAX_IDENT_LEN = 20;
    public const int MAX_STRING_LEN = 400;
    public const int MAX_STACK_SIZE = 100;
    public const int MAX_NEST_LEVEL = 20;
}