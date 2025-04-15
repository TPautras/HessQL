using System.Linq;
using HessQLParser.Parser.CustomEnumerators;
using HessQLParser.Parser.Expressions;

namespace HessQLParser.Parser.Statements
{
    /// <summary>
    /// Représente un élément de la clause ORDER BY avec son expression et sa direction.
    /// </summary>
    public class OrderByItem
    {
        public IExpression Expression { get; set; }
        public bool Ascending { get; set; }

        /// <summary>
        /// Constructeur d’OrderByItem.
        /// </summary>
        public OrderByItem(IExpression expression, bool ascending)
        {
            Expression = expression;
            Ascending = ascending;
        }

        /// <summary>
        /// Renvoie une chaîne de débuggage.
        /// </summary>
        public string Debug()
        {
            return Expression.Debug() + (Ascending ? " ASC" : " DESC");
        }
    }

    /// <summary>
    /// Représente une instruction SELECT avec toutes les options.
    /// </summary>
    public class SelectStmt : StatementEnumerator, IStatement
    {
        public bool IsDistinct { get; set; }
        public int? Top { get; set; }
        public List<IExpression> Columns { get; set; }
        public IExpression Table { get; set; }
        public IExpression? WhereClause { get; set; }
        public List<IExpression>? GroupBy { get; set; }
        public IExpression? Having { get; set; }
        public List<OrderByItem>? OrderBy { get; set; }
        public int? Limit { get; set; }
        public int? Offset { get; set; }

        /// <summary>
        /// Constructeur de SelectStmt.
        /// </summary>
        public SelectStmt(bool isDistinct, int? top, List<IExpression> columns, IExpression table, IExpression? whereClause, List<IExpression>? groupBy, IExpression? having, List<OrderByItem>? orderBy, int? limit, int? offset)
        {
            IsDistinct = isDistinct;
            Top = top;
            Columns = columns;
            Table = table;
            WhereClause = whereClause;
            GroupBy = groupBy;
            Having = having;
            OrderBy = orderBy;
            Limit = limit;
            Offset = offset;
        }

        public void Statement()
        {
        }

        /// <summary>
        /// Méthode de débuggage qui affiche l’état interne du SELECT.
        /// </summary>
        public string Debug()
        {
            string res = "SelectStmt { ";
            if (IsDistinct)
                res += "DISTINCT ";
            if (Top.HasValue)
                res += $"TOP {Top.Value} ";
            res += "Columns: [ " + string.Join(", ", Columns.Select(c => c.Debug())) + " ], ";
            res += "Table: " + Table.Debug();
            if (WhereClause != null)
                res += ", Where: " + WhereClause.Debug();
            if (GroupBy != null && GroupBy.Count > 0)
                res += ", GroupBy: [ " + string.Join(", ", GroupBy.Select(g => g.Debug())) + " ]";
            if (Having != null)
                res += ", Having: " + Having.Debug();
            if (OrderBy != null && OrderBy.Count > 0)
                res += ", OrderBy: [ " + string.Join(", ", OrderBy.Select(o => o.Debug())) + " ]";
            if (Limit.HasValue)
                res += ", Limit: " + Limit.Value;
            if (Offset.HasValue)
                res += ", Offset: " + Offset.Value;
            res += " }";
            return res;
        }

        /// <summary>
        /// Analyse une instruction SELECT avec les options DISTINCT, TOP, liste de colonnes, FROM, WHERE, GROUP BY, HAVING, ORDER BY, LIMIT et OFFSET.
        /// </summary>
        public static IStatement ParseSelectStmt(Parser parser)
        {
            // Consommer le token SELECT
            parser.Advance();

            // OPTION DISTINCT
            bool isDistinct = false;
            if (parser.CurrentTokenKind() == Token.TokenTypes.DISTINCT)
            {
                isDistinct = true;
                parser.Advance();
            }

            // OPTION TOP
            int? topValue = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.TOP)
            {
                parser.Advance();
                var topToken = parser.Expect(Token.TokenTypes.INTEGER_LITERAL);
                if (!int.TryParse(topToken.Value, out int parsedTop))
                    throw new Exception("La valeur TOP doit être un entier");
                topValue = parsedTop;
            }

            // Liste des colonnes
            List<IExpression> columns = new List<IExpression>();
            if (parser.CurrentTokenKind() == Token.TokenTypes.ASTERISK)
            {
                columns.Add(new SymbolExpression { Value = "*" });
                parser.Advance();
            }
            else
            {
                while (true)
                {
                    IExpression expr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
                    columns.Add(expr);
                    if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                        parser.Advance();
                    else
                        break;
                }
            }

            // FROM clause
            parser.Expect(Token.TokenTypes.FROM);
            IExpression tableExpr = ParseExpressions.ParsePrimaryExpr(parser);

            // WHERE clause optionnelle
            IExpression? whereExpr = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.WHERE)
            {
                parser.Advance();
                whereExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
            }

            // GROUP BY clause optionnelle
            List<IExpression>? groupBy = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.GROUP_BY)
            {
                parser.Advance();
                groupBy = new List<IExpression>();
                while (true)
                {
                    IExpression groupExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
                    groupBy.Add(groupExpr);
                    if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                        parser.Advance();
                    else
                        break;
                }
            }

            // HAVING clause optionnelle
            IExpression? havingExpr = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.HAVING)
            {
                parser.Advance();
                havingExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
            }

            // ORDER BY clause optionnelle
            List<OrderByItem>? orderBy = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.ORDER_BY)
            {
                parser.Advance();
                orderBy = new List<OrderByItem>();
                while (true)
                {
                    IExpression orderExpr = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
                    bool ascending = true;
                    if (parser.CurrentTokenKind() == Token.TokenTypes.IDENTIFIER)
                    {
                        string ident = parser.Peek().Value.ToUpper();
                        if (ident == "ASC" || ident == "DESC")
                        {
                            var orderToken = parser.Advance();
                            ascending = orderToken.Value.ToUpper() != "DESC";
                        }
                    }
                    orderBy.Add(new OrderByItem(orderExpr, ascending));
                    if (parser.CurrentTokenKind() == Token.TokenTypes.COMMA)
                        parser.Advance();
                    else
                        break;
                }
            }

            // LIMIT clause optionnelle
            int? limitValue = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.LIMIT)
            {
                parser.Advance();
                var limitToken = parser.Expect(Token.TokenTypes.INTEGER_LITERAL);
                if (!int.TryParse(limitToken.Value, out int parsedLimit))
                    throw new Exception("La valeur LIMIT doit être un entier");
                limitValue = parsedLimit;
            }

            // OFFSET clause optionnelle
            int? offsetValue = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.OFFSET)
            {
                parser.Advance();
                var offsetToken = parser.Expect(Token.TokenTypes.INTEGER_LITERAL);
                if (!int.TryParse(offsetToken.Value, out int parsedOffset))
                    throw new Exception("La valeur OFFSET doit être un entier");
                offsetValue = parsedOffset;
            }

            // Consommer le point-virgule final si présent
            if (parser.CurrentTokenKind() == Token.TokenTypes.SEMICOLON)
                parser.Advance();
            return new SelectStmt(isDistinct, topValue, columns, tableExpr, whereExpr, groupBy, havingExpr, orderBy, limitValue, offsetValue);
        }
    }
}
