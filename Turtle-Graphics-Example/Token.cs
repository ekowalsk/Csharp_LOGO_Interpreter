using System.Text;
public class Token {
    public Token(Dictionary.LexemeType t, string v, Position p){
        type = t;
        value = v;
        position = p;
    }
    public Dictionary.LexemeType getType(){
        return type;
    }
    public string getValue(){
        return value;
    }
    public (int, int) getPosition(){
        return position.getPosition();
    }
    private Dictionary.LexemeType type;
    private string value;
    private Position position;



}