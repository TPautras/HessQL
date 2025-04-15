using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class DropStmt : StatementEnumerator, IStatement
    {
        public IExpression Table { get; set; }
        public DropStmt(IExpression table)
        {
            Table = table;
        }
        public void Statement() { }
        public string Debug()
        {
            return "DropStmt { Table: " + Table.Debug() + " }";
        }
        public static IStatement ParseDropStmt(Parser parser)
        {
            parser.Advance();
            parser.Expect(Token.TokenTypes.TABLE);
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new DropStmt(tableExpr);
        }
    }
}