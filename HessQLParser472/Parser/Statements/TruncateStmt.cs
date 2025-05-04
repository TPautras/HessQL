using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class TruncateStmt : IStatement
    {
        public IExpression Table { get; set; }
        public TruncateStmt(IExpression table)
        {
            Table = table;
        }
        public void Statement() { }
        public string Debug()
        {
            return "TruncateStmt { Table: " + Table.Debug() + " }";
        }
        public static IStatement ParseTruncateStmt(Parser parser)
        {
            parser.Advance();
            parser.Expect(Token.TokenTypes.TABLE);
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new TruncateStmt(tableExpr);
        }
    }
}