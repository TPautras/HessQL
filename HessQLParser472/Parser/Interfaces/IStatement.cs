namespace HessQLParser.Parser
{
    /// <summary>
    /// Types of Statements :
    /// BlockStmt
    ///     List of statements <list type="IStatement"></list>
    /// ExpressionStmt
    ///     Expression IExpression
    /// VarDeclStatement
    ///     VariableName string
    ///     Assigned value IExpression
    /// </summary>
    public interface IStatement
    {
        void Statement();
        string Debug();
    }
}