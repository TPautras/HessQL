namespace HessQLParser.Parser;
/// <summary>
/// Types of Expressions :
/// 
/// Assignment expression
///     IExpression Assignee
///     Token Operator
///     IExpression Value
/// 
/// Binary Expression
///     IExpression LeftExpression
///     Token.TokenType Operator
///     IExpression RightExpression
/// 
/// Number Expression
///     float value
/// 
/// Symbol Expression
///     string Value
/// 
/// String Expression
///     string Value
/// 
/// Prefix Expression
///     Token Operator
///     IExpression RightExpression
/// 
/// </summary>
public interface IExpression
{
    public void Expression();
    public string Debug();
}