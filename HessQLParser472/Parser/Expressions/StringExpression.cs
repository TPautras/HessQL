using System;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Expressions
{
    public class StringExpression : ExpressionEnumerator, IExpression
    {
        public string Value;
    
        public void Expression()
        {
            throw new NotImplementedException();
        }

        public string Debug()
        {
            string res = "type: StringExpression{";
            res += "Value : " + Value;
            return res+"}";
        }
    }
}