using System.Linq.Expressions;

namespace HessQLParser.Parser.Statements;

public class ExpressionStmt : IStatement
{
    public IExpression Expression { get; set; }
    
    public ExpressionStmt(IExpression expression)
    {
        Expression = expression;
    }
    
    public void Statement()
    {
        
    }

    public string Debug()
    {
        return "type: ExpressionStmt{"+Expression.Debug()+"}";
    }
}