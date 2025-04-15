using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Expressions;

public class NumberExpression : ExpressionEnumerator, IExpression
{
    public float Value;

    public void Expression()
    {
        
    }

    public string Debug()
    {
        string res = "type: NumericExpression{";
        res += "Value : " + Value;
        return res+"}";;
    }
}