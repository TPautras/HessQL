namespace HessQLParser.Parser.Statements;

public class SelectStmt : IStatement
{
    private BlockStatement block = null;


    public void Statement()
    {
        throw new NotImplementedException();
    }

    public string Debug()
    {
        string res = "type: SelectStmt{}";
        return res;
    }

    public static IStatement ParseSelectStmt(Parser parser)
    {
        parser.Expect(Token.TokenTypes.IDENTIFIER);
        throw new NotImplementedException("GAAAAAAAAAMBEEEERGE");
    }
}