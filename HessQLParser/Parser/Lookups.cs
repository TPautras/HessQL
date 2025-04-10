namespace HessQLParser.Parser;

public class Lookups
{
    public enum BindingPower
    {
        Lowest = 0,

        // Expressions booléennes
        Or = 10,
        And = 20,
        Not = 30,

        // Comparateurs
        Equals = 40,              // =
        NotEquals = 40,           // != or <>
        LessGreater = 40,         // <, >, <=, >=

        // IN, LIKE, BETWEEN
        In = 45,
        Between = 45,
        Like = 45,

        // Concaténation (rare, ex: 'a' || 'b')
        Concat = 50,

        // Mathématiques
        AddSubtract = 60,         // +, -
        MultiplyDivide = 70,      // *, /
        Unary = 80,               // -a, +a

        // Appels de fonction, accès aux colonnes (postfixe)
        Call = 90,
        MemberAccess = 100,       // ex: table.col

        Highest = 255
    }

    public static IStatement StatementHandler(Parser parser)
    {
        
    }

    public static IExpression NudHandler(Parser parser)
    {
        
    }

    public static IExpression LedHandler(Parser parser, IExpression left, BindingPower bp)
    {
        
    }
}