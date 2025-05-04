using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class DeleteStmt : IStatement
    {
        public IExpression Table { get; set; }
        public IExpression WhereClause { get; set; }
        public DeleteStmt(IExpression table, IExpression whereClause)
        {
            Table = table;
            WhereClause = whereClause;
        }
        public void Statement() { }
        public string Debug()
        {
            string res = "DeleteStmt { Table: " + Table.Debug();
            if (WhereClause != null)
                res += ", Where: " + WhereClause.Debug();
            res += " }";
            return res;
        }
        public static IStatement ParseDeleteStmt(Parser parser)
        {
            parser.Advance();
            parser.Expect(Token.TokenTypes.FROM);
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            IExpression whereExpr = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.WHERE)
            {
                parser.Advance();
                whereExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
            }
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new DeleteStmt(tableExpr, whereExpr);
        }
    }
}