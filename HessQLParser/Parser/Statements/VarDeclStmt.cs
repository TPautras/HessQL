using HessQLParser.Parser.Expressions;

namespace HessQLParser.Parser.Statements;

public class VarDeclStmt : IStatement
{
    public string VarName;
    public  IExpression AssignedValue;

    public VarDeclStmt(string varName, IExpression assignedValue)
    {
        VarName = varName;
        AssignedValue = assignedValue;
    }

    public void Statement()
    {
        throw new NotImplementedException();
    }

    public string Debug()
    {
        string res = "type: Variable declaration statement{";
        res += "VariableName: \"" + VarName + "\"{";
        res+= AssignedValue.Debug()+"}";
        return res+"}";
    }

    public static IStatement ParseVarDeclStmt(Parser parser)
    {
        parser.Advance();
        string varName = parser.ExpectError(Token.TokenTypes.IDENTIFIER,"Expected to find variable name after a variable declaration statement").Value;
        parser.Expect(Token.TokenTypes.ASSIGNMENT);
        IExpression assignedValue = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Highest);
        return new VarDeclStmt(
            varName,
            assignedValue
        );
    }
}