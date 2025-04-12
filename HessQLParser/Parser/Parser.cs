using HessQLParser.Parser.Statements;

namespace HessQLParser.Parser;

public class Parser
{
    private List<Token> _tokens = new List<Token>();
    private int _position;

    public Parser(int position)
    {
        this._position = position;
    }
    
    public static Parser CreateParser(int position, List<Token> tokens)
    {
        Lookups.CreateTokenLookups();
        Parser parser = new Parser(position);
        parser._tokens = tokens;
        return parser;
    }

    public static BlockStatement Parse(List<Token> tokens)
    {
        List<IStatement> body = new List<IStatement>();
        Parser p = CreateParser(0, tokens);
        while (!p.IsAtEnd())
        {
            var statements = body.Append(ParseStatements.ParseStatement(p));
        }
        
        return new BlockStatement(body);
    }

    public Token Peek()
    {
        return _tokens[_position];
    }

    public Token Advance()
    {
        Token tk = Peek();
        _position++;
        return tk;
    }

    public bool IsAtEnd()
    {
        return _position < _tokens.Count &&
               Equals(CurrentTokenKind,
                   Token.TokenTypes.END_OF_FILE);
    }

    public Token.TokenTypes CurrentTokenKind()
    {
        return Peek().TokenType;
    }
    
    public Token ExpectError(Token.TokenTypes expectedKind, string errorMessage = "Unexpected token")
    {
        var token = Peek();
        var kind = token.TokenType;

        if (kind != expectedKind)
        {
            throw new Exception($"{errorMessage}. Expected: {expectedKind}, but got: {kind}");
        }

        return Advance();
    }

    public Token Expect(Token.TokenTypes expectedKind)
    {
        return this.ExpectError(expectedKind);
    }

}