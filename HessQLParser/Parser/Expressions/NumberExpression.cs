namespace HessQLParser.Parser.Expressions;

public class NumberExpression :IExpression
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