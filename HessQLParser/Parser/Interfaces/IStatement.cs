namespace HessQLParser.Parser;
/// <summary>
/// Types of Statements :
/// BlockStatement
///     List of statements <list type="IStatement"></list>
/// ExpressionStatement
///     Expression IExpression
/// VarDeclStatement
///     VariableName string
///     Assigned value IExpression
/// </summary>
public interface IStatement
{
    public void Statement();
    public string Debug();
}