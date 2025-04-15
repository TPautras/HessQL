using System.Linq;
using HessQLParser.Parser.CustomEnumerators;
using HessQLParser.Parser.Expressions;

namespace HessQLParser.Parser.Statements
{
    /// <summary>
    /// Représente une instruction INSERT.
    /// </summary>
    public class InsertStmt : IStatement
    {
        public IExpression Table { get; set; }
        public List<string>? Columns { get; set; }
        public List<IExpression> Values { get; set; }

        /// <summary>
        /// Constructeur d’InsertStmt.
        /// </summary>
        public InsertStmt(IExpression table, List<string>? columns, List<IExpression> values)
        {
            Table = table;
            Columns = columns;
            Values = values;
        }

        public void Statement()
        {
        }

        /// <summary>
        /// Méthode de débuggage qui affiche l’état interne de l’INSERT.
        /// </summary>
        public string Debug()
        {
            string res = "InsertStmt { Table: " + Table.Debug();
            if (Columns != null && Columns.Count > 0)
                res += ", Columns: [ " + string.Join(", ", Columns) + " ]";
            res += ", Values: [ " + string.Join(", ", Values.Select(v => v.Debug())) + " ] }";
            return res;
        }

        /// <summary>
        /// Analyse une instruction INSERT sous la forme :
        /// INSERT [INTO] &lt;table&gt; [( &lt;colonnes&gt; )] VALUES ( &lt;valeurs&gt; )
        /// </summary>
        public static IStatement ParseInsertStmt(Parser parser)
        {
            parser.Advance();
            if (parser.CurrentTokenKind() == Token.TokenTypes.INTO)
                parser.Advance();
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);
            List<string>? columns = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.LEFT_PAREN)
            {
                parser.Advance();
                columns = new List<string>();
                while (parser.CurrentTokenKind() != Token.TokenTypes.RIGHT_PAREN)
                {
                    var colToken = parser.Expect(Token.TokenTypes.IDENTIFIER);
                    columns.Add(colToken.Value);
                    if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                        parser.Advance();
                    else
                        break;
                }
                parser.Expect(Token.TokenTypes.RIGHT_PAREN);
            }
            parser.Expect(Token.TokenTypes.VALUES);
            parser.Expect(Token.TokenTypes.LEFT_PAREN);
            List<IExpression> values = new List<IExpression>();
            while (parser.CurrentTokenKind() != Token.TokenTypes.RIGHT_PAREN)
            {
                IExpression valueExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
                values.Add(valueExpr);
                if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                    parser.Advance();
                else
                    break;
            }
            parser.Expect(Token.TokenTypes.RIGHT_PAREN);
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new InsertStmt(tableExpr, columns, values);
        }
    }
}
