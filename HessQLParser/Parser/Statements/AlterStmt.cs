using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class AlterStmt : StatementEnumerator, IStatement
    {
        public IExpression Table { get; set; }
        public IExpression NewTable { get; set; }
        public AlterStmt(IExpression table, IExpression newTable)
        {
            Table = table;
            NewTable = newTable;
        }
        public void Statement() { }
        public string Debug()
        {
            return "AlterStmt { Table: " + Table.Debug() + ", NewTable: " + NewTable.Debug() + " }";
        }
        public static IStatement ParseAlterStmt(Parser parser)
        {
            parser.Advance();
            parser.Expect(Token.TokenTypes.TABLE);
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            if (parser.CurrentTokenKind() == Token.TokenTypes.IDENTIFIER && parser.Peek().Value.ToUpper() == "RENAME")
                parser.Advance();
            else
                throw new ParsingException(ParserErrors.AlterMissingRename);
            if (parser.CurrentTokenKind() == Token.TokenTypes.IDENTIFIER && parser.Peek().Value.ToUpper() == "TO")
                parser.Advance();
            else
                throw new ParsingException(ParserErrors.AlterMissingTo);
            IExpression newTableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new AlterStmt(tableExpr, newTableExpr);
        }
    }
}