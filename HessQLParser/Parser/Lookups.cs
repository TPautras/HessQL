using HessQLParser.Parser.Expressions;
using HessQLParser.Parser.Statements;

namespace HessQLParser.Parser;

public class Lookups
{
    public static readonly Dictionary<Token.TokenTypes, BindingPower> BpLookup = new();
    public static readonly Dictionary<Token.TokenTypes, NudHandler> NudLookup = new();
    public static readonly Dictionary<Token.TokenTypes, LedHandler> LedLookup = new();
    public static readonly Dictionary<Token.TokenTypes, StmtHandler?> StmtLookup = new();

    public enum BindingPower
    {
        Lowest = 0,

        // Expressions booléennes
        Or = 10,
        And = 20,
        Not = 30,

        // Comparateurs
        Equals = 40,
        NotEquals = 40,
        LessGreater = 40,

        // IN, LIKE, BETWEEN
        In = 45,
        Between = 45,
        Like = 45,

        // Concaténation
        Concat = 50,

        // Mathématiques
        AddSubtract = 60,
        MultiplyDivide = 70,
        Unary = 80,

        // Postfixes
        Call = 90,
        MemberAccess = 100,

        Highest = 255
    }

    public delegate IStatement StmtHandler(Parser parser);
    public delegate IExpression NudHandler(Parser parser);
    public delegate IExpression LedHandler(Parser parser, IExpression left, BindingPower bp);

    private static void Led(Token.TokenTypes kind, BindingPower bp, LedHandler handler)
    {
        BpLookup[kind] = bp;
        LedLookup[kind] = handler;
    }

    private static void Nud(Token.TokenTypes kind, BindingPower bp, NudHandler handler)
    {
        BpLookup[kind] = BindingPower.Highest;
        NudLookup[kind] = handler;
    }

    private static void Stmt(Token.TokenTypes kind, StmtHandler handler)
    {
        BpLookup[kind] = BindingPower.Lowest;
        StmtLookup[kind] = handler;
    }

    public static void CreateTokenLookups()
    {
        // Assignments
        //Led(Token.TokenTypes.ASSIGNMENT, BindingPower.Equals, ParserMethods.ParseAssignmentExpr);
        //Led(Token.TokenTypes.ADD_EQUALS, BindingPower.Equals, ParserMethods.ParseAssignmentExpr);
        //Led(Token.TokenTypes.SUBTRACT_EQUALS, BindingPower.Equals, ParserMethods.ParseAssignmentExpr);

        // Logical
        Led(Token.TokenTypes.AND, BindingPower.And, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.OR, BindingPower.Or, ParseExpressions.ParseBinaryExpression);

        // Relational
        Led(Token.TokenTypes.LESS_THAN, BindingPower.LessGreater, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.LESS_THAN_OR_EQUAL, BindingPower.LessGreater, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.GREATER_THAN, BindingPower.LessGreater, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.GREATER_THAN_OR_EQUAL, BindingPower.LessGreater, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.EQUALS, BindingPower.Equals, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.NOT_EQUALS, BindingPower.NotEquals, ParseExpressions.ParseBinaryExpression);

        // Math
        Led(Token.TokenTypes.PLUS, BindingPower.AddSubtract, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.MINUS, BindingPower.AddSubtract, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.SLASH, BindingPower.MultiplyDivide, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.ASTERISK, BindingPower.MultiplyDivide, ParseExpressions.ParseBinaryExpression);
        Led(Token.TokenTypes.PERCENT, BindingPower.MultiplyDivide, ParseExpressions.ParseBinaryExpression);

        // Literals
        Nud(Token.TokenTypes.INTEGER_LITERAL, BindingPower.Highest, ParseExpressions.ParsePrimaryExpr);
        Nud(Token.TokenTypes.REAL_LITERAL, BindingPower.Highest, ParseExpressions.ParsePrimaryExpr);
        Nud(Token.TokenTypes.STRING_LITERAL, BindingPower.Highest, ParseExpressions.ParsePrimaryExpr);
        Nud(Token.TokenTypes.IDENTIFIER, BindingPower.Highest, ParseExpressions.ParsePrimaryExpr);

        // Unary/Prefix
        //Nud(Token.TokenTypes.MINUS, BindingPower.Unary, ParserMethods.ParsePrefixExpr);
        //Nud(Token.TokenTypes.NOT, BindingPower.Unary, ParserMethods.ParsePrefixExpr);
        //Nud(Token.TokenTypes.LEFT_BRACKET, BindingPower.Highest, ParserMethods.ParseArrayLiteralExpr);

        // Member / Call
        //Led(Token.TokenTypes.DOT, BindingPower.MemberAccess, ParserMethods.ParseMemberExpr);
        //Led(Token.TokenTypes.LEFT_BRACKET, BindingPower.MemberAccess, ParserMethods.ParseMemberExpr);
        //Led(Token.TokenTypes.LEFT_PAREN, BindingPower.Call, ParserMethods.ParseCallExpr);

        // Grouping
        //Nud(Token.TokenTypes.LEFT_PAREN, BindingPower.Lowest, ParserMethods.ParseGroupingExpr);

        // Function expr
        //Nud(Token.TokenTypes.FUNCTION, BindingPower.Lowest, ParserMethods.ParseFnExpr);
        

        // Statements (adapt to your grammar)
        Stmt(Token.TokenTypes.SELECT, SelectStmt.ParseSelectStmt);
        Stmt(Token.TokenTypes.DECLARE, VarDeclStmt.ParseVarDeclStmt);
        //Stmt(Token.TokenTypes.FROM, ParserMethods.ParseSelectStmt);
    }
}
