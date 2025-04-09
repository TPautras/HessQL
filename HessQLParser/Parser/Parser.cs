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
            body.Append(StatementParser.ParseStatement(p));
        }
        
        return new BlockStatement(body);
    }

    private Token Peek()
    {
        return _tokens[_position];
    }

    private Token Advance()
    {
        Token tk = Peek();
        _position++;
        return tk;
    }

    private bool IsAtEnd()
    {
        return _position < _tokens.Count &&
               Equals(CurrentTokenKind,
                   Token.TokenTypes.END_OF_FILE);
    }

    private Token.TokenTypes CurrentTokenKind()
    {
        return Peek().TokenType;
    }
}