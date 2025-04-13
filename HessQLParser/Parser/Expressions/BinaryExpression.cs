using System.Linq.Expressions;

namespace HessQLParser.Parser.Expressions;

public class BinaryExpression :IExpression
{
    public BinaryExpression(IExpression leftExpression, Token.TokenTypes @operator, IExpression rightExpression)
    {
        LeftExpression = leftExpression;
        Operator = @operator;
        RightExpression = rightExpression;
    }

    public IExpression LeftExpression { get; set; }
    public Token.TokenTypes Operator { get; set; }
    public IExpression RightExpression { get; set; }

    public void Expression()
    {
        
    }
}