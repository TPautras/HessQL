using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
namespace HessQLParser.Parser.Statements
{
    public class ColumnDefinition
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public ColumnDefinition(string columnName, string dataType)
        {
            ColumnName = columnName;
            DataType = dataType;
        }
        public string Debug()
        {
            return ColumnName + " " + DataType;
        }
    }
    public class CreateTableStmt : IStatement
    {
        public IExpression Table { get; set; }
        public List<ColumnDefinition> Columns { get; set; }
        public CreateTableStmt(IExpression table, List<ColumnDefinition> columns)
        {
            Table = table;
            Columns = columns;
        }
        public void Statement() { }
        public string Debug()
        {
            string res = "CreateTableStmt { Table: " + Table.Debug();
            res += ", Columns: [ " + string.Join(", ", Columns.Select(c => c.Debug())) + " ] }";
            return res;
        }
        public static IStatement ParseCreateTableStmt(Parser parser)
        {
            parser.Advance();
            parser.Expect(Token.TokenTypes.TABLE);
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            parser.Expect(Token.TokenTypes.LEFT_PAREN);
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            while (true)
            {
                var colToken = parser.Expect(Token.TokenTypes.IDENTIFIER);
                string columnName = colToken.Value;
                var typeToken = parser.Expect(Token.TokenTypes.IDENTIFIER);
                string dataType = typeToken.Value;
                columns.Add(new ColumnDefinition(columnName, dataType));
                if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                    parser.Advance();
                else
                    break;
            }
            parser.Expect(Token.TokenTypes.RIGHT_PAREN);
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new CreateTableStmt(tableExpr, columns);
        }
    }
}
