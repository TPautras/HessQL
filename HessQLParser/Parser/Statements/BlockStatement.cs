namespace HessQLParser.Parser.Statements;

public class BlockStatement(List<IStatement> statements) : IStatement
{
    public List<IStatement> body = statements;

    public void Statement()
    {
        
    }
}