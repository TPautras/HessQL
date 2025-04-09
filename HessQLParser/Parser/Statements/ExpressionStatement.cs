using System.Linq.Expressions;

namespace HessQLParser.Parser.Statements;

public class ExpressionStatement : IStatement
{
    public IExpression Expression;

    public void Statement()
    {
        
    }
}