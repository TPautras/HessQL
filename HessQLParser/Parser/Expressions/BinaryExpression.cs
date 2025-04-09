using System.Linq.Expressions;

namespace HessQLParser.Parser.Expressions;

public class BinaryExpression :IExpression
{
    public IExpression LeftExpression;
    public Token.TokenTypes Operator;
    public IExpression RightExpression;

    public void Expression()
    {
        
    }
}