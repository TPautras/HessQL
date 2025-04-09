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
        tokensTypes.Add(TokenTypes.QUOTED_IDENTIFIER);
        tokensTypes.Add(TokenTypes.VARIABLE);
        tokensTypes.Add(TokenTypes.INTEGER_LITERAL);
        tokensTypes.Add(TokenTypes.REAL_LITERAL);
        tokensTypes.Add(TokenTypes.STRING_LITERAL);
        tokensTypes.Add(TokenTypes.BINARY_LITERAL);
        tokensTypes.Add(TokenTypes.NULL_LITERAL);
        tokensTypes.Add(TokenTypes.DATE_LITERAL);
        tokensTypes.Add(TokenTypes.BOOLEAN_LITERAL);
        tokensTypes.Add(TokenTypes.COMMENT_SINGLE_LINE);
        
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