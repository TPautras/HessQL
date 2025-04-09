namespace HessQLParser.Parser.Statements;

public class BlockStatement(List<IStatement> statements) : IStatement
{
    public List<IStatement> Statements = statements;

    public void Statement()
    {
        
    }
}