using System;
using System.Linq.Expressions;

namespace HessQLParser.Parser.Expressions
{
    public class SymbolExpression : IExpression
    {
        public string Value { get; set; }

        public void Expression()
        {
            throw new NotImplementedException();
        }

        public string Debug()
        {
            string res = "type: SymbolExpression{";
            res += "Value : " + Value;
            return res+"}";
        }
    }
}