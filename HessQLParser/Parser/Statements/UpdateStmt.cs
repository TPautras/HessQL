using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class UpdateAssignment
    {
        public string Column { get; set; }
        public IExpression Expression { get; set; }
        public UpdateAssignment(string column, IExpression expression)
        {
            Column = column;
            Expression = expression;
        }
        public string Debug()
        {
            return Column + " = " + Expression.Debug();
        }
    }

    public class UpdateStmt : StatementEnumerator, IStatement
    {
        public IExpression Table { get; set; }
        public List<UpdateAssignment> Assignments { get; set; }
        public IExpression? WhereClause { get; set; }
        public UpdateStmt(IExpression table, List<UpdateAssignment> assignments, IExpression? whereClause)
        {
            Table = table;
            Assignments = assignments;
            WhereClause = whereClause;
        }
        public void Statement() { }
        public string Debug()
        {
            string res = "UpdateStmt { Table: " + Table.Debug();
            res += ", Assignments: [ " + string.Join(", ", Assignments.Select(a => a.Debug())) + " ]";
            if (WhereClause != null)
                res += ", Where: " + WhereClause.Debug();
            res += " }";
            return res;
        }
        public static IStatement ParseUpdateStmt(Parser parser)
        {
            parser.Advance();
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            parser.Expect(Token.TokenTypes.SET);
            List<UpdateAssignment> assignments = new List<UpdateAssignment>();
            while (true)
            {
                var colToken = parser.Expect(Token.TokenTypes.IDENTIFIER);
                string columnName = colToken.Value;
                parser.Expect(Token.TokenTypes.EQUALS);
                IExpression expr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
                assignments.Add(new UpdateAssignment(columnName, expr));
                if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                    parser.Advance();
                else
                    break;
            }
            IExpression? whereExpr = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.WHERE)
            {
                parser.Advance();
                whereExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
            }
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new UpdateStmt(tableExpr, assignments, whereExpr);
        }
    }
}
