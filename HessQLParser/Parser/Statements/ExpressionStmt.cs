using System.Linq.Expressions;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements;

public class ExpressionStmt : StatementEnumerator, IStatement
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