namespace HessQLParser.Parser.Statements;

public class BlockStatement : IStatement
{
    public List<IStatement> body;

    public BlockStatement(List<IStatement> statements)
    {
        body = statements;
    }

    public void Statement()
    {
        
    }
}