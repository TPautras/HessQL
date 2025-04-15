using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Expressions;

public class PrefixExpression : ExpressionEnumerator, IExpression
{ 
    public PrefixExpression(Token @operator, IExpression rightExpression)
    {
        Operator = @operator;
        RightExpression = rightExpression;
    }

    public Token Operator { get; set; }
    public IExpression RightExpression { get; set; }
    
    public void Expression()
    {
        throw new NotImplementedException();
    }

    public string Debug()
    {
        throw new NotImplementedException();
    }

    public static IExpression ParsePrefixExpression(Parser parser)
    {
        throw new NotImplementedException();
    }
}