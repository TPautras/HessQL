using HessQLParser.Parser;
using HessQLParser.Parser.Statements;

namespace HessQLInterpreter;

public class Interpreter
{
    public void interpretCaller(BlockStmt parsedStatement)
    {
        foreach (IStatement statement in parsedStatement)
        {
            
        }
    }
}