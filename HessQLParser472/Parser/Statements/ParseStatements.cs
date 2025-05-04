using System.Collections.Generic;
using HessQLParser.Parser.Expressions;

namespace HessQLParser.Parser.Statements
{
    public class ParseStatements
    {
        public static IStatement ParseStatement(Parser parser)
        {
            Lookups.StmtHandler stmtFn = null;
            try
            {
                stmtFn = Lookups.StmtLookup[parser.CurrentTokenKind()];
            }
            catch (KeyNotFoundException) {}

            if (stmtFn != null)
            {
                return stmtFn(parser);
            }

            var expression = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
            parser.Expect(Token.TokenTypes.SEMICOLON);
        
            return new ExpressionStmt(expression);
        }
    }
}
