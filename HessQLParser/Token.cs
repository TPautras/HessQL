namespace HessQLParser;

public class Token
{ 
    public TokenTypes TokenType;
    public string Value;

    public Token(string value, TokenTypes type)
    {
        this.Value = value;
        this.TokenType = type;
    }

    public enum TokenTypes
    {
        Test, 
        test, 
        ak
    }
    
    
}