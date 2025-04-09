namespace HessQLParser;

public partial class Token(string value, Token.TokenTypes type)
{ 
    public TokenTypes TokenType = type;
    public string Value = value;

    public static Token NewToken(string value, Token.TokenTypes type)
    {
        return new Token(value,type);
    }

    public bool IsOneOfMany(List<TokenTypes> tokensTypes)
    {
        return tokensTypes.Contains(this.TokenType);
    }

    public static string Debug(Token token)
    {
        List<TokenTypes> tokensTypes = new List<TokenTypes>();
        tokensTypes.Add(TokenTypes.IDENTIFIER);
        tokensTypes.Add(TokenTypes.NUMERIC);
        tokensTypes.Add(TokenTypes.STRING_LITERAL);
        string res = "";
        if (token.IsOneOfMany(tokensTypes))
            res = (TokenKindToString(token.TokenType) + ": " + token.Value);
        else
        {
           res = TokenKindToString(token.TokenType);
        }

        return res;
    }
}