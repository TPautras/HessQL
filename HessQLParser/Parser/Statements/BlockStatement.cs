namespace HessQLParser.Parser.Statements;

public class BlockStatement : IStatement
{
    public List<IStatement> body { get; set; }

    public BlockStatement(List<IStatement> statements)
    {
        body = statements;
    }

    public void Statement()
    {
        
    }

    public string Debug()
    {
        string res = "type: blockStatement{";

        foreach (IStatement statement in body)
        {
            res += statement.Debug();
        }
        
        return res+"}";
    }
}