using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HessQLParser;

public partial class Tokenizer
{
    public List<RegexPattern> RegexPatterns = new List<RegexPattern>();
    public List<Token>? Tokens;
    public string Source;
    public int Position;

    public Tokenizer(List<Token> tokens, string source, int position)
    {
        Tokens = tokens;
        this.Source = source;
        this.Position = position;
    }
    private Tokenizer(string source)
    {
        this.Source = source;
        this.Position = 0;
        this.Tokens = new List<Token>();
        this.RegexPatterns = new List<RegexPattern>
        {
            new RegexPattern(new Regex(@"[0-9]+(\.[0-9]+)?"), NumericHandler()),
            new RegexPattern(new Regex(@"SELECT"), DefaultHandler(Token.TokenTypes.SELECT, "SELECT")),
            new RegexPattern(new Regex(@"INSERT"), DefaultHandler(Token.TokenTypes.INSERT, "INSERT")),
            new RegexPattern(new Regex(@"UPDATE"), DefaultHandler(Token.TokenTypes.UPDATE, "UPDATE")),
            new RegexPattern(new Regex(@"DELETE"), DefaultHandler(Token.TokenTypes.DELETE, "DELETE")),
            new RegexPattern(new Regex(@"CREATE"), DefaultHandler(Token.TokenTypes.CREATE, "CREATE")),
            new RegexPattern(new Regex(@"ALTER"), DefaultHandler(Token.TokenTypes.ALTER, "ALTER")),
            new RegexPattern(new Regex(@"DROP"), DefaultHandler(Token.TokenTypes.DROP, "DROP")),
            new RegexPattern(new Regex(@"TRUNCATE"), DefaultHandler(Token.TokenTypes.TRUNCATE, "TRUNCATE")),
            new RegexPattern(new Regex(@"FROM"), DefaultHandler(Token.TokenTypes.FROM, "FROM")),
            new RegexPattern(new Regex(@"WHERE"), DefaultHandler(Token.TokenTypes.WHERE, "WHERE")),
            new RegexPattern(new Regex(@"GROUP BY"), DefaultHandler(Token.TokenTypes.GROUP_BY, "GROUP BY")),
            new RegexPattern(new Regex(@"ORDER BY"), DefaultHandler(Token.TokenTypes.ORDER_BY, "ORDER BY")),
            new RegexPattern(new Regex(@"JOIN"), DefaultHandler(Token.TokenTypes.JOIN, "JOIN")),
            new RegexPattern(new Regex(@"INNER"), DefaultHandler(Token.TokenTypes.INNER, "INNER")),
            new RegexPattern(new Regex(@"LEFT"), DefaultHandler(Token.TokenTypes.LEFT, "LEFT")),
            new RegexPattern(new Regex(@"RIGHT"), DefaultHandler(Token.TokenTypes.RIGHT, "RIGHT")),
            new RegexPattern(new Regex(@"FULL"), DefaultHandler(Token.TokenTypes.FULL, "FULL")),
            new RegexPattern(new Regex(@"CROSS"), DefaultHandler(Token.TokenTypes.CROSS, "CROSS")),
            new RegexPattern(new Regex(@"ON"), DefaultHandler(Token.TokenTypes.ON, "ON")),
            new RegexPattern(new Regex(@"USING"), DefaultHandler(Token.TokenTypes.USING, "USING")),
            new RegexPattern(new Regex(@"AS"), DefaultHandler(Token.TokenTypes.AS, "AS")),
            new RegexPattern(new Regex(@"LIMIT"), DefaultHandler(Token.TokenTypes.LIMIT, "LIMIT")),
            new RegexPattern(new Regex(@"OFFSET"), DefaultHandler(Token.TokenTypes.OFFSET, "OFFSET")),
            new RegexPattern(new Regex(@"DISTINCT"), DefaultHandler(Token.TokenTypes.DISTINCT, "DISTINCT")),
            new RegexPattern(new Regex(@"TABLE"), DefaultHandler(Token.TokenTypes.TABLE, "TABLE")),
            new RegexPattern(new Regex(@"VIEW"), DefaultHandler(Token.TokenTypes.VIEW, "VIEW")),
            new RegexPattern(new Regex(@"INDEX"), DefaultHandler(Token.TokenTypes.INDEX, "INDEX")),
            new RegexPattern(new Regex(@"TRIGGER"), DefaultHandler(Token.TokenTypes.TRIGGER, "TRIGGER")),
            new RegexPattern(new Regex(@"PROCEDURE"), DefaultHandler(Token.TokenTypes.PROCEDURE, "PROCEDURE")),
            new RegexPattern(new Regex(@"FUNCTION"), DefaultHandler(Token.TokenTypes.FUNCTION, "FUNCTION")),
            new RegexPattern(new Regex(@"SCHEMA"), DefaultHandler(Token.TokenTypes.SCHEMA, "SCHEMA")),
            new RegexPattern(new Regex(@"DATABASE"), DefaultHandler(Token.TokenTypes.DATABASE, "DATABASE")),
            new RegexPattern(new Regex(@"COLUMN"), DefaultHandler(Token.TokenTypes.COLUMN, "COLUMN")),
            new RegexPattern(new Regex(@"CONSTRAINT"), DefaultHandler(Token.TokenTypes.CONSTRAINT, "CONSTRAINT")),
            new RegexPattern(new Regex(@"PRIMARY KEY"), DefaultHandler(Token.TokenTypes.PRIMARY_KEY, "PRIMARY KEY")),
            new RegexPattern(new Regex(@"FOREIGN KEY"), DefaultHandler(Token.TokenTypes.FOREIGN_KEY, "FOREIGN KEY")),
            new RegexPattern(new Regex(@"CHECK"), DefaultHandler(Token.TokenTypes.CHECK, "CHECK")),
            new RegexPattern(new Regex(@"DEFAULT"), DefaultHandler(Token.TokenTypes.DEFAULT, "DEFAULT")),
            new RegexPattern(new Regex(@"UNIQUE"), DefaultHandler(Token.TokenTypes.UNIQUE, "UNIQUE")),
            new RegexPattern(new Regex(@"CASE"), DefaultHandler(Token.TokenTypes.CASE, "CASE")),
            new RegexPattern(new Regex(@"WHEN"), DefaultHandler(Token.TokenTypes.WHEN, "WHEN")),
            new RegexPattern(new Regex(@"THEN"), DefaultHandler(Token.TokenTypes.THEN, "THEN")),
            new RegexPattern(new Regex(@"ELSE"), DefaultHandler(Token.TokenTypes.ELSE, "ELSE")),
            new RegexPattern(new Regex(@"ELSEIF"), DefaultHandler(Token.TokenTypes.ELSEIF, "ELSEIF")),
            new RegexPattern(new Regex(@"OPTION"), DefaultHandler(Token.TokenTypes.OPTION, "OPTION")),
            new RegexPattern(new Regex(@"RECONFIGURE"), DefaultHandler(Token.TokenTypes.RECONFIGURE, "RECONFIGURE")),
            new RegexPattern(new Regex(@"USE"), DefaultHandler(Token.TokenTypes.USE, "USE")),
            new RegexPattern(new Regex(@"SET"), DefaultHandler(Token.TokenTypes.SET, "SET")),
            new RegexPattern(new Regex(@"DECLARE"), DefaultHandler(Token.TokenTypes.DECLARE, "DECLARE")),
            new RegexPattern(new Regex(@"PRINT"), DefaultHandler(Token.TokenTypes.PRINT, "PRINT")),
            new RegexPattern(new Regex(@"EXEC"), DefaultHandler(Token.TokenTypes.EXEC, "EXEC")),
            new RegexPattern(new Regex(@"EXECUTE"), DefaultHandler(Token.TokenTypes.EXECUTE, "EXECUTE")),
            new RegexPattern(new Regex(@"OPEN"), DefaultHandler(Token.TokenTypes.OPEN, "OPEN")),
            new RegexPattern(new Regex(@"CLOSE"), DefaultHandler(Token.TokenTypes.CLOSE, "CLOSE")),
            new RegexPattern(new Regex(@"DEALLOCATE"), DefaultHandler(Token.TokenTypes.DEALLOCATE, "DEALLOCATE")),
            new RegexPattern(new Regex(@"CURSOR"), DefaultHandler(Token.TokenTypes.CURSOR, "CURSOR")),
            new RegexPattern(new Regex(@"FETCH"), DefaultHandler(Token.TokenTypes.FETCH, "FETCH")),
            new RegexPattern(new Regex(@"NEXT"), DefaultHandler(Token.TokenTypes.NEXT, "NEXT")),
            new RegexPattern(new Regex(@"INTO CURSOR"), DefaultHandler(Token.TokenTypes.INTO_CURSOR, "INTO CURSOR")),
            new RegexPattern(new Regex(@"GO"), DefaultHandler(Token.TokenTypes.GO, "GO")),
            new RegexPattern(new Regex(@"NOLOCK"), DefaultHandler(Token.TokenTypes.NOLOCK, "NOLOCK")),
            new RegexPattern(new Regex(@"INT"), DefaultHandler(Token.TokenTypes.INT, "INT")),
            new RegexPattern(new Regex(@"BIGINT"), DefaultHandler(Token.TokenTypes.BIGINT, "BIGINT")),
            new RegexPattern(new Regex(@"SMALLINT"), DefaultHandler(Token.TokenTypes.SMALLINT, "SMALLINT")),
            new RegexPattern(new Regex(@"TINYINT"), DefaultHandler(Token.TokenTypes.TINYINT, "TINYINT")),
            new RegexPattern(new Regex(@"FLOAT"), DefaultHandler(Token.TokenTypes.FLOAT, "FLOAT")),
            new RegexPattern(new Regex(@"DECIMAL"), DefaultHandler(Token.TokenTypes.DECIMAL, "DECIMAL")),
            new RegexPattern(new Regex(@"NUMERIC"), DefaultHandler(Token.TokenTypes.NUMERIC, "NUMERIC")),
            new RegexPattern(new Regex(@"MONEY"), DefaultHandler(Token.TokenTypes.MONEY, "MONEY")),
            new RegexPattern(new Regex(@"SMALLMONEY"), DefaultHandler(Token.TokenTypes.SMALLMONEY, "SMALLMONEY")),
            new RegexPattern(new Regex(@"CHAR"), DefaultHandler(Token.TokenTypes.CHAR, "CHAR")),
            new RegexPattern(new Regex(@"NCHAR"), DefaultHandler(Token.TokenTypes.NCHAR, "NCHAR")),
            new RegexPattern(new Regex(@"TEXT"), DefaultHandler(Token.TokenTypes.TEXT, "TEXT")),
            new RegexPattern(new Regex(@"NTEXT"), DefaultHandler(Token.TokenTypes.NTEXT, "NTEXT")),
            new RegexPattern(new Regex(@"VARBINARY"), DefaultHandler(Token.TokenTypes.VARBINARY, "VARBINARY")),
            new RegexPattern(new Regex(@"IMAGE"), DefaultHandler(Token.TokenTypes.IMAGE, "IMAGE")),
            new RegexPattern(new Regex(@"DATE"), DefaultHandler(Token.TokenTypes.DATE, "DATE")),
            new RegexPattern(new Regex(@"DATETIME"), DefaultHandler(Token.TokenTypes.DATETIME, "DATETIME")),
            new RegexPattern(new Regex(@"SMALLDATETIME"), DefaultHandler(Token.TokenTypes.SMALLDATETIME, "SMALLDATETIME")),
            new RegexPattern(new Regex(@"DATETIME2"), DefaultHandler(Token.TokenTypes.DATETIME2, "DATETIME2")),
            new RegexPattern(new Regex(@"DATETIMEOFFSET"), DefaultHandler(Token.TokenTypes.DATETIMEOFFSET, "DATETIMEOFFSET")),
            new RegexPattern(new Regex(@"GEOGRAPHY"), DefaultHandler(Token.TokenTypes.GEOGRAPHY, "GEOGRAPHY")),
            new RegexPattern(new Regex(@"GEOMETRY"), DefaultHandler(Token.TokenTypes.GEOMETRY, "GEOMETRY")),
            new RegexPattern(new Regex(@"HIERARCHYID"), DefaultHandler(Token.TokenTypes.HIERARCHYID, "HIERARCHYID")),
            new RegexPattern(new Regex(@"SQL_VARIANT"), DefaultHandler(Token.TokenTypes.SQL_VARIANT, "SQL_VARIANT")),
            new RegexPattern(new Regex(@"TIMESTAMP"), DefaultHandler(Token.TokenTypes.TIMESTAMP, "TIMESTAMP")),
            new RegexPattern(new Regex(@"ROWVERSION"), DefaultHandler(Token.TokenTypes.ROWVERSION, "ROWVERSION")),
            new RegexPattern(new Regex(@"UNIQUEIDENTIFIER"), DefaultHandler(Token.TokenTypes.UNIQUEIDENTIFIER, "UNIQUEIDENTIFIER")),
            new RegexPattern(new Regex(@"FOR XML"), DefaultHandler(Token.TokenTypes.FOR_XML, "FOR XML")),
            new RegexPattern(new Regex(@"FOR JSON"), DefaultHandler(Token.TokenTypes.FOR_JSON, "FOR JSON")),
            new RegexPattern(new Regex(@"OPENJSON"), DefaultHandler(Token.TokenTypes.OPEN_JSON, "OPENJSON")),
            new RegexPattern(new Regex(@"\+"), DefaultHandler(Token.TokenTypes.PLUS, "+")),
            new RegexPattern(new Regex(@"-"), DefaultHandler(Token.TokenTypes.MINUS, "-")),
            new RegexPattern(new Regex(@"\*"), DefaultHandler(Token.TokenTypes.ASTERISK, "*")),
            new RegexPattern(new Regex(@"/"), DefaultHandler(Token.TokenTypes.SLASH, "/")),
            new RegexPattern(new Regex(@"%"), DefaultHandler(Token.TokenTypes.PERCENT, "%")),
            new RegexPattern(new Regex(@"="), DefaultHandler(Token.TokenTypes.EQUALS, "=")),
            new RegexPattern(new Regex(@"!="), DefaultHandler(Token.TokenTypes.NOT_EQUALS, "!=")),
            new RegexPattern(new Regex(@">"), DefaultHandler(Token.TokenTypes.GREATER_THAN, ">")),
            new RegexPattern(new Regex(@"<"), DefaultHandler(Token.TokenTypes.LESS_THAN, "<")),
            new RegexPattern(new Regex(@">="), DefaultHandler(Token.TokenTypes.GREATER_THAN_OR_EQUAL, ">=")),
            new RegexPattern(new Regex(@"<="), DefaultHandler(Token.TokenTypes.LESS_THAN_OR_EQUAL, "<=")),
            new RegexPattern(new Regex(@"\("), DefaultHandler(Token.TokenTypes.LEFT_PAREN, "(")),
            new RegexPattern(new Regex(@"\)"), DefaultHandler(Token.TokenTypes.RIGHT_PAREN, ")")),
            new RegexPattern(new Regex(@","), DefaultHandler(Token.TokenTypes.COMMA, ",")),
            new RegexPattern(new Regex(@";"), DefaultHandler(Token.TokenTypes.SEMICOLON, ";"))
        };
    }
    
    public delegate void RegexHandler(Tokenizer tokenizer, Match match);
    
    private void AdvanceN(int n)
    {
        this.Position += n;
    }

    private void Push(Token token)
    {
        this.Tokens?.Add(token);
    }

    private byte At()
    {
        return (byte)this.Source[this.Position];
    }

    private string Remainder()
    {
        return this.Source.Substring(this.Position);
    }

    private bool AtEnd()
    {
        return this.Position >= this.Source.Length;
    }
    
    public static List<Token>? Tokenize(string source)
    {
        Tokenizer tokenizer = new Tokenizer(source);

        while (!tokenizer.AtEnd())
        {
            bool matched = false;

            foreach (RegexPattern regexPattern in tokenizer.RegexPatterns)
            {
                var match = regexPattern.Regex.Match(tokenizer.Remainder());
                int? startIndex = null;
                int? endIndex = null;
                if (match.Success)
                {
                    startIndex = match.Index;
                    endIndex = match.Index + match.Length;
                }

                var loc = new int?[] { startIndex, endIndex };
                
                if (loc.Length != 0 && loc[0] == 0)
                {
                    regexPattern.Handler(tokenizer, match);
                    matched = true;
                    break;
                }
            }
            
            tokenizer.Push(Token.NewToken("END OF FILE", Token.TokenTypes.END_OF_FILE));
            if (!matched)
                throw new Exception("Tokenize::Error -> Unexpected Token"); //TODO : Ajouter la localisation de l'erreur et un type d'erreur specifique
        }
        
        
        return tokenizer.Tokens;
    }
    
    private static RegexHandler DefaultHandler(Token.TokenTypes kind, string value)
    {
        return (Tokenizer tokenizer, Match _) =>
        {
            tokenizer.AdvanceN(value.Length);
            tokenizer.Push(Token.NewToken(value, kind));
        };
    }
    
    private static RegexHandler NumericHandler()
    {
        
        return (Tokenizer tokenizer, Match match ) =>
        {
            string matchedValue = match.Value;
            tokenizer.Push(Token.NewToken( matchedValue, Token.TokenTypes.INTEGER_LITERAL));
            tokenizer.AdvanceN(matchedValue.Length);
        };
    }
}
