using System.Linq.Expressions;

namespace HessQLParser.Parser.Statements;

public class ExpressionStatement : IStatement
{
    public IExpression Expression { get; set; }
    
    public ExpressionStatement(IExpression expression)
    {
        Expression = expression;
    }
    
    public void Statement()
    {
        
    }
}