using System.Linq;
using HessQLParser.Parser.Expressions;
using HessQLParser.Errors;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class UseStmt : IStatement
    {
        public string Database { get; set; }
        public UseStmt(string database)
        {
            Database = database;
        }
        public void Statement() { }
        public string Debug()
        {
            return "UseStmt { Database: " + Database + " }";
        }
        public static IStatement ParseUseStmt(Parser parser)
        {
            parser.Advance();
            var dbToken = parser.Expect(Token.TokenTypes.IDENTIFIER);
            string database = dbToken.Value;
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new UseStmt(database);
        }
    }
}