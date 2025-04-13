namespace HessQLParser.Parser.Statements;

public class CreateStatement : IStatement
{
    private BlockStatement block = null;


    public void Statement()
    {
        throw new NotImplementedException();
    }

    public string Debug()
    {
        string res = "type: CreateStatement{}";
        return res;
    }
}