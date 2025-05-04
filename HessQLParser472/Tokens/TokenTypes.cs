using System;
using System.Collections.Generic;

namespace HessQLParser
{
    public partial class Token
    {
        public enum TokenTypes
        {
            // Spéciaux
            UNKNOWN,
            END_OF_FILE,
            WHITESPACE,
            COMMENT_SINGLE_LINE,
            COMMENT_MULTI_LINE,

            // Identifiants et littéraux
            IDENTIFIER,
            QUOTED_IDENTIFIER,
            VARIABLE,
            GLOBAL_VARIABLE,
            INTEGER_LITERAL,
            REAL_LITERAL,
            STRING_LITERAL,
            BINARY_LITERAL,
            NULL_LITERAL,
            DATE_LITERAL,
            BOOLEAN_LITERAL,

            // Opérateurs arithmétiques
            PLUS,
            MINUS,
            ASTERISK,
            SLASH,
            PERCENT,
            EQUALS,
            NOT_EQUALS,
            GREATER_THAN,
            LESS_THAN,
            GREATER_THAN_OR_EQUAL,
            LESS_THAN_OR_EQUAL,
            DOUBLE_EQUALS,

            // Opérateurs logiques
            AND,
            OR,
            NOT,
            IN,
            BETWEEN,
            LIKE,
            IS,
            EXISTS,
            ALL,
            ANY,
            SOME,

            // Affectation et opérateurs composés
            ASSIGNMENT,
            ADD_EQUALS,
            SUBTRACT_EQUALS,
            MULTIPLY_EQUALS,
            DIVIDE_EQUALS,
            MODULO_EQUALS,
            BITWISE_AND_EQUALS,
            BITWISE_OR_EQUALS,
            BITWISE_XOR_EQUALS,

            // Opérateurs binaires
            BITWISE_AND,
            BITWISE_OR,
            BITWISE_XOR,
            BITWISE_NOT,
            SHIFT_LEFT,
            SHIFT_RIGHT,

            // Délimiteurs et ponctuation
            LEFT_PAREN,
            RIGHT_PAREN,
            LEFT_BRACKET,
            RIGHT_BRACKET,
            COMMA,
            DOT,
            SEMICOLON,
            COLON,
            DOUBLE_COLON,
            AT_SIGN,
            QUESTION_MARK,

            // Mots-clés de contrôle de flux
            BEGIN,
            END,
            IF,
            ELSE,
            WHILE,
            FOR,
            CONTINUE,
            BREAK,
            GOTO,
            RETURN,
            WAITFOR,
            THROW,
            TRY,
            CATCH,

            // Mots-clés DDL
            CREATE,
            ALTER,
            DROP,
            TRUNCATE,
            RENAME,

            // Mots-clés DML
            SELECT,
            INSERT,
            UPDATE,
            DELETE,
            MERGE,
            VALUES,
            INTO,
            OUTPUT,

            // Mots-clés transactionnels
            COMMIT,
            ROLLBACK,
            SAVE,
            TRANSACTION,
            BEGIN_TRANSACTION,

            // Clauses SQL
            FROM,
            WHERE,
            GROUP_BY,
            HAVING,
            ORDER_BY,
            TOP,
            LIMIT,
            OFFSET,
            DISTINCT,
            UNION,
            UNION_ALL,
            INTERSECT,
            EXCEPT,
            JOIN,
            INNER,
            LEFT,
            RIGHT,
            FULL,
            CROSS,
            ON,
            USING,
            AS,

            // Objets SQL
            TABLE,
            VIEW,
            INDEX,
            TRIGGER,
            PROCEDURE,
            FUNCTION,
            SCHEMA,
            DATABASE,
            COLUMN,
            CONSTRAINT,
            PRIMARY_KEY,
            FOREIGN_KEY,
            CHECK,
            DEFAULT,
            UNIQUE,

            // Autres mots-clés
            CASE,
            WHEN,
            THEN,
            ELSEIF,
            WITH,
            OPTION,
            RECONFIGURE,
            USE,
            SET,
            DECLARE,
            PRINT,
            EXEC,
            EXECUTE,
            OPEN,
            CLOSE,
            DEALLOCATE,
            CURSOR,
            FETCH,
            NEXT,
            INTO_CURSOR,
            GO,
            NOLOCK,

            // Types de données
            INT,
            BIGINT,
            SMALLINT,
            TINYINT,
            BIT,
            FLOAT,
            REAL,
            DECIMAL,
            NUMERIC,
            MONEY,
            SMALLMONEY,
            CHAR,
            NCHAR,
            VARCHAR,
            NVARCHAR,
            TEXT,
            NTEXT,
            BINARY,
            VARBINARY,
            IMAGE,
            DATE,
            TIME,
            DATETIME,
            SMALLDATETIME,
            DATETIME2,
            DATETIMEOFFSET,
            XML,
            GEOGRAPHY,
            GEOMETRY,
            HIERARCHYID,
            SQL_VARIANT,
            TIMESTAMP,
            ROWVERSION,
            UNIQUEIDENTIFIER,

            // XML & JSON
            FOR_XML,
            FOR_JSON,
            OPEN_JSON
        }

        private sealed class TokenTypeEqualityComparer : IEqualityComparer<Token>
        {
            public bool Equals(Token x, Token y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x == null || y == null) return false;
                return x.TokenType == y.TokenType;
            }

            public int GetHashCode(Token obj)
            {
                return (int)obj.TokenType;
            }
        }

        public static IEqualityComparer<Token> TokenTypeComparer { get; } = new TokenTypeEqualityComparer();

        public static readonly Dictionary<TokenTypes, string> TokenKindToStringMap = new Dictionary<TokenTypes, string>();

        static Token()
        {
            TokenKindToStringMap.Add(TokenTypes.UNKNOWN, "UNKNOWN");
            TokenKindToStringMap.Add(TokenTypes.END_OF_FILE, "END OF FILE");
            TokenKindToStringMap.Add(TokenTypes.WHITESPACE, "WHITESPACE");
            TokenKindToStringMap.Add(TokenTypes.COMMENT_SINGLE_LINE, "COMMENT SINGLE LINE");
            TokenKindToStringMap.Add(TokenTypes.COMMENT_MULTI_LINE, "COMMENT MULTI LINE");
            TokenKindToStringMap.Add(TokenTypes.IDENTIFIER, "IDENTIFIER");
            TokenKindToStringMap.Add(TokenTypes.QUOTED_IDENTIFIER, "QUOTED IDENTIFIER");
            TokenKindToStringMap.Add(TokenTypes.VARIABLE, "VARIABLE");
            TokenKindToStringMap.Add(TokenTypes.GLOBAL_VARIABLE, "GLOBAL VARIABLE");
            TokenKindToStringMap.Add(TokenTypes.INTEGER_LITERAL, "integer");
            TokenKindToStringMap.Add(TokenTypes.REAL_LITERAL, "real");
            TokenKindToStringMap.Add(TokenTypes.STRING_LITERAL, "string");
            TokenKindToStringMap.Add(TokenTypes.NULL_LITERAL, "null");
            TokenKindToStringMap.Add(TokenTypes.DATE_LITERAL, "date");
            TokenKindToStringMap.Add(TokenTypes.BOOLEAN_LITERAL, "boolean");
            TokenKindToStringMap.Add(TokenTypes.PLUS, "+");
            TokenKindToStringMap.Add(TokenTypes.MINUS, "-");
            TokenKindToStringMap.Add(TokenTypes.ASTERISK, "*");
            TokenKindToStringMap.Add(TokenTypes.SLASH, "/");
            TokenKindToStringMap.Add(TokenTypes.PERCENT, "%");
            TokenKindToStringMap.Add(TokenTypes.EQUALS, "=");
            TokenKindToStringMap.Add(TokenTypes.NOT_EQUALS, "!=");
            TokenKindToStringMap.Add(TokenTypes.GREATER_THAN, ">");
            TokenKindToStringMap.Add(TokenTypes.LESS_THAN, "<");
            TokenKindToStringMap.Add(TokenTypes.GREATER_THAN_OR_EQUAL, ">=");
            TokenKindToStringMap.Add(TokenTypes.LESS_THAN_OR_EQUAL, "<=");
            TokenKindToStringMap.Add(TokenTypes.DOUBLE_EQUALS, "DOUBLE EQUALS");
            TokenKindToStringMap.Add(TokenTypes.AND, "AND");
            TokenKindToStringMap.Add(TokenTypes.OR, "OR");
            TokenKindToStringMap.Add(TokenTypes.NOT, "NOT");
            TokenKindToStringMap.Add(TokenTypes.SELECT, "SELECT");
            TokenKindToStringMap.Add(TokenTypes.INSERT, "INSERT");
            TokenKindToStringMap.Add(TokenTypes.UPDATE, "UPDATE");
            TokenKindToStringMap.Add(TokenTypes.DELETE, "DELETE");
            TokenKindToStringMap.Add(TokenTypes.CREATE, "CREATE");
            TokenKindToStringMap.Add(TokenTypes.ALTER, "ALTER");
            TokenKindToStringMap.Add(TokenTypes.DROP, "DROP");
            TokenKindToStringMap.Add(TokenTypes.TRUNCATE, "TRUNCATE");
            TokenKindToStringMap.Add(TokenTypes.FROM, "FROM");
            TokenKindToStringMap.Add(TokenTypes.WHERE, "WHERE");
            TokenKindToStringMap.Add(TokenTypes.GROUP_BY, "GROUP BY");
            TokenKindToStringMap.Add(TokenTypes.ORDER_BY, "ORDER BY");
            TokenKindToStringMap.Add(TokenTypes.JOIN, "JOIN");
            TokenKindToStringMap.Add(TokenTypes.INNER, "INNER");
            TokenKindToStringMap.Add(TokenTypes.LEFT, "LEFT");
            TokenKindToStringMap.Add(TokenTypes.RIGHT, "RIGHT");
            TokenKindToStringMap.Add(TokenTypes.FULL, "FULL");
            TokenKindToStringMap.Add(TokenTypes.CROSS, "CROSS");
            TokenKindToStringMap.Add(TokenTypes.ON, "ON");
            TokenKindToStringMap.Add(TokenTypes.USING, "USING");
            TokenKindToStringMap.Add(TokenTypes.AS, "AS");
            TokenKindToStringMap.Add(TokenTypes.LIMIT, "LIMIT");
            TokenKindToStringMap.Add(TokenTypes.OFFSET, "OFFSET");
            TokenKindToStringMap.Add(TokenTypes.DISTINCT, "DISTINCT");
            TokenKindToStringMap.Add(TokenTypes.TABLE, "TABLE");
            TokenKindToStringMap.Add(TokenTypes.VIEW, "VIEW");
            TokenKindToStringMap.Add(TokenTypes.INDEX, "INDEX");
            TokenKindToStringMap.Add(TokenTypes.TRIGGER, "TRIGGER");
            TokenKindToStringMap.Add(TokenTypes.PROCEDURE, "PROCEDURE");
            TokenKindToStringMap.Add(TokenTypes.FUNCTION, "FUNCTION");
            TokenKindToStringMap.Add(TokenTypes.SCHEMA, "SCHEMA");
            TokenKindToStringMap.Add(TokenTypes.DATABASE, "DATABASE");
            TokenKindToStringMap.Add(TokenTypes.COLUMN, "COLUMN");
            TokenKindToStringMap.Add(TokenTypes.CONSTRAINT, "CONSTRAINT");
            TokenKindToStringMap.Add(TokenTypes.PRIMARY_KEY, "PRIMARY KEY");
            TokenKindToStringMap.Add(TokenTypes.FOREIGN_KEY, "FOREIGN KEY");
            TokenKindToStringMap.Add(TokenTypes.VALUES, "VALUES");
            TokenKindToStringMap.Add(TokenTypes.CHECK, "CHECK");
            TokenKindToStringMap.Add(TokenTypes.DEFAULT, "DEFAULT");
            TokenKindToStringMap.Add(TokenTypes.UNIQUE, "UNIQUE");
            TokenKindToStringMap.Add(TokenTypes.CASE, "CASE");
            TokenKindToStringMap.Add(TokenTypes.WHEN, "WHEN");
            TokenKindToStringMap.Add(TokenTypes.THEN, "THEN");
                        TokenKindToStringMap.Add(TokenTypes.RETURN, "RETURN");
            TokenKindToStringMap.Add(TokenTypes.WAITFOR, "WAITFOR");
            TokenKindToStringMap.Add(TokenTypes.THROW, "THROW");
            TokenKindToStringMap.Add(TokenTypes.TRY, "TRY");
            TokenKindToStringMap.Add(TokenTypes.CATCH, "CATCH");
            TokenKindToStringMap.Add(TokenTypes.CREATE, "CREATE");
            TokenKindToStringMap.Add(TokenTypes.ALTER, "ALTER");
            TokenKindToStringMap.Add(TokenTypes.DROP, "DROP");
            TokenKindToStringMap.Add(TokenTypes.TRUNCATE, "TRUNCATE");
            TokenKindToStringMap.Add(TokenTypes.RENAME, "RENAME");
            TokenKindToStringMap.Add(TokenTypes.SELECT, "SELECT");
            TokenKindToStringMap.Add(TokenTypes.INSERT, "INSERT");
            TokenKindToStringMap.Add(TokenTypes.UPDATE, "UPDATE");
            TokenKindToStringMap.Add(TokenTypes.DELETE, "DELETE");
            TokenKindToStringMap.Add(TokenTypes.MERGE, "MERGE");
            TokenKindToStringMap.Add(TokenTypes.VALUES, "VALUES");
            TokenKindToStringMap.Add(TokenTypes.INTO, "INTO");
            TokenKindToStringMap.Add(TokenTypes.OUTPUT, "OUTPUT");
            TokenKindToStringMap.Add(TokenTypes.COMMIT, "COMMIT");
            TokenKindToStringMap.Add(TokenTypes.ROLLBACK, "ROLLBACK");
            TokenKindToStringMap.Add(TokenTypes.SAVE, "SAVE");
            TokenKindToStringMap.Add(TokenTypes.TRANSACTION, "TRANSACTION");
            TokenKindToStringMap.Add(TokenTypes.BEGIN_TRANSACTION, "BEGIN TRANSACTION");
            TokenKindToStringMap.Add(TokenTypes.FROM, "FROM");
            TokenKindToStringMap.Add(TokenTypes.WHERE, "WHERE");
            TokenKindToStringMap.Add(TokenTypes.GROUP_BY, "GROUP BY");
            TokenKindToStringMap.Add(TokenTypes.HAVING, "HAVING");
            TokenKindToStringMap.Add(TokenTypes.ORDER_BY, "ORDER BY");
            TokenKindToStringMap.Add(TokenTypes.TOP, "TOP");
            TokenKindToStringMap.Add(TokenTypes.LIMIT, "LIMIT");
            TokenKindToStringMap.Add(TokenTypes.OFFSET, "OFFSET");
            TokenKindToStringMap.Add(TokenTypes.DISTINCT, "DISTINCT");
            TokenKindToStringMap.Add(TokenTypes.UNION, "UNION");
            TokenKindToStringMap.Add(TokenTypes.UNION_ALL, "UNION ALL");
            TokenKindToStringMap.Add(TokenTypes.INTERSECT, "INTERSECT");
            TokenKindToStringMap.Add(TokenTypes.EXCEPT, "EXCEPT");
            TokenKindToStringMap.Add(TokenTypes.JOIN, "JOIN");
            TokenKindToStringMap.Add(TokenTypes.INNER, "INNER");
            TokenKindToStringMap.Add(TokenTypes.LEFT, "LEFT");
            TokenKindToStringMap.Add(TokenTypes.RIGHT, "RIGHT");
            TokenKindToStringMap.Add(TokenTypes.FULL, "FULL");
            TokenKindToStringMap.Add(TokenTypes.CROSS, "CROSS");
            TokenKindToStringMap.Add(TokenTypes.ON, "ON");
            TokenKindToStringMap.Add(TokenTypes.USING, "USING");
            TokenKindToStringMap.Add(TokenTypes.AS, "AS");
            TokenKindToStringMap.Add(TokenTypes.TABLE, "TABLE");
            TokenKindToStringMap.Add(TokenTypes.VIEW, "VIEW");
            TokenKindToStringMap.Add(TokenTypes.INDEX, "INDEX");
            TokenKindToStringMap.Add(TokenTypes.TRIGGER, "TRIGGER");
            TokenKindToStringMap.Add(TokenTypes.PROCEDURE, "PROCEDURE");
            TokenKindToStringMap.Add(TokenTypes.FUNCTION, "FUNCTION");
            TokenKindToStringMap.Add(TokenTypes.SCHEMA, "SCHEMA");
            TokenKindToStringMap.Add(TokenTypes.DATABASE, "DATABASE");
            TokenKindToStringMap.Add(TokenTypes.COLUMN, "COLUMN");
            TokenKindToStringMap.Add(TokenTypes.CONSTRAINT, "CONSTRAINT");
            TokenKindToStringMap.Add(TokenTypes.PRIMARY_KEY, "PRIMARY KEY");
            TokenKindToStringMap.Add(TokenTypes.FOREIGN_KEY, "FOREIGN KEY");
            TokenKindToStringMap.Add(TokenTypes.CHECK, "CHECK");
            TokenKindToStringMap.Add(TokenTypes.DEFAULT, "DEFAULT");
            TokenKindToStringMap.Add(TokenTypes.UNIQUE, "UNIQUE");
            TokenKindToStringMap.Add(TokenTypes.CASE, "CASE");
            TokenKindToStringMap.Add(TokenTypes.WHEN, "WHEN");
            TokenKindToStringMap.Add(TokenTypes.THEN, "THEN");
            TokenKindToStringMap.Add(TokenTypes.ELSE, "ELSE");
            TokenKindToStringMap.Add(TokenTypes.ELSEIF, "ELSEIF");
            TokenKindToStringMap.Add(TokenTypes.WITH, "WITH");
            TokenKindToStringMap.Add(TokenTypes.OPTION, "OPTION");
            TokenKindToStringMap.Add(TokenTypes.RECONFIGURE, "RECONFIGURE");
            TokenKindToStringMap.Add(TokenTypes.USE, "USE");
            TokenKindToStringMap.Add(TokenTypes.SET, "SET");
            TokenKindToStringMap.Add(TokenTypes.DECLARE, "DECLARE");
            TokenKindToStringMap.Add(TokenTypes.PRINT, "PRINT");
            TokenKindToStringMap.Add(TokenTypes.EXEC, "EXEC");
            TokenKindToStringMap.Add(TokenTypes.EXECUTE, "EXECUTE");
            TokenKindToStringMap.Add(TokenTypes.OPEN, "OPEN");
            TokenKindToStringMap.Add(TokenTypes.CLOSE, "CLOSE");
            TokenKindToStringMap.Add(TokenTypes.DEALLOCATE, "DEALLOCATE");
            TokenKindToStringMap.Add(TokenTypes.CURSOR, "CURSOR");
            TokenKindToStringMap.Add(TokenTypes.FETCH, "FETCH");
            TokenKindToStringMap.Add(TokenTypes.NEXT, "NEXT");
            TokenKindToStringMap.Add(TokenTypes.INTO_CURSOR, "INTO CURSOR");
            TokenKindToStringMap.Add(TokenTypes.GO, "GO");
            TokenKindToStringMap.Add(TokenTypes.NOLOCK, "NOLOCK");
            TokenKindToStringMap.Add(TokenTypes.INT, "INT");
            TokenKindToStringMap.Add(TokenTypes.BIGINT, "BIGINT");
            TokenKindToStringMap.Add(TokenTypes.SMALLINT, "SMALLINT");
            TokenKindToStringMap.Add(TokenTypes.TINYINT, "TINYINT");
            TokenKindToStringMap.Add(TokenTypes.BIT, "BIT");
            TokenKindToStringMap.Add(TokenTypes.FLOAT, "FLOAT");
            TokenKindToStringMap.Add(TokenTypes.REAL, "REAL");
            TokenKindToStringMap.Add(TokenTypes.DECIMAL, "DECIMAL");
            TokenKindToStringMap.Add(TokenTypes.NUMERIC, "NUMERIC");
            TokenKindToStringMap.Add(TokenTypes.MONEY, "MONEY");
            TokenKindToStringMap.Add(TokenTypes.SMALLMONEY, "SMALLMONEY");
            TokenKindToStringMap.Add(TokenTypes.CHAR, "CHAR");
            TokenKindToStringMap.Add(TokenTypes.NCHAR, "NCHAR");
            TokenKindToStringMap.Add(TokenTypes.VARCHAR, "VARCHAR");
            TokenKindToStringMap.Add(TokenTypes.NVARCHAR, "NVARCHAR");
            TokenKindToStringMap.Add(TokenTypes.TEXT, "TEXT");
            TokenKindToStringMap.Add(TokenTypes.NTEXT, "NTEXT");
            TokenKindToStringMap.Add(TokenTypes.BINARY, "BINARY");
            TokenKindToStringMap.Add(TokenTypes.VARBINARY, "VARBINARY");
            TokenKindToStringMap.Add(TokenTypes.IMAGE, "IMAGE");
            TokenKindToStringMap.Add(TokenTypes.DATE, "DATE");
            TokenKindToStringMap.Add(TokenTypes.TIME, "TIME");
            TokenKindToStringMap.Add(TokenTypes.DATETIME, "DATETIME");
            TokenKindToStringMap.Add(TokenTypes.SMALLDATETIME, "SMALLDATETIME");
            TokenKindToStringMap.Add(TokenTypes.DATETIME2, "DATETIME2");
            TokenKindToStringMap.Add(TokenTypes.DATETIMEOFFSET, "DATETIMEOFFSET");
            TokenKindToStringMap.Add(TokenTypes.XML, "XML");
            TokenKindToStringMap.Add(TokenTypes.GEOGRAPHY, "GEOGRAPHY");
            TokenKindToStringMap.Add(TokenTypes.GEOMETRY, "GEOMETRY");
            TokenKindToStringMap.Add(TokenTypes.HIERARCHYID, "HIERARCHYID");
            TokenKindToStringMap.Add(TokenTypes.SQL_VARIANT, "SQL_VARIANT");
            TokenKindToStringMap.Add(TokenTypes.TIMESTAMP, "TIMESTAMP");
            TokenKindToStringMap.Add(TokenTypes.ROWVERSION, "ROWVERSION");
            TokenKindToStringMap.Add(TokenTypes.UNIQUEIDENTIFIER, "UNIQUEIDENTIFIER");
            TokenKindToStringMap.Add(TokenTypes.FOR_XML, "FOR XML");
            TokenKindToStringMap.Add(TokenTypes.FOR_JSON, "FOR JSON");
            TokenKindToStringMap.Add(TokenTypes.OPEN_JSON, "OPENJSON");
        }

        public static string TokenKindToString(TokenTypes tokenKind)
        {
            string value;
            return TokenKindToStringMap.TryGetValue(tokenKind, out value) ? value : tokenKind.ToString();
        }
    }
}

 
