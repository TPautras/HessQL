using System.Linq.Expressions;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Expressions
{
    public class BinaryExpression :ExpressionEnumerator, IExpression
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

        public string Debug()
        {
            string res = "type: BinaryExpression{";
            res += "Operator { " + "Value :"+Token.TokenKindToString(Operator)+"}";
            res+= "LeftExpression {" + LeftExpression.Debug()+"}";
            res += "RightExpression { " + RightExpression.Debug()+"}";
            return res+"}";
        }
    }
}