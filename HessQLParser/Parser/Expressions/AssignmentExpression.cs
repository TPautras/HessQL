using System.Collections;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Expressions;

public class AssignmentExpression : ExpressionEnumerator, IExpression
{
    public AssignmentExpression(IExpression assignee, Token @operator, IExpression value)
    {
        Assignee = assignee;
        Operator = @operator;
        Value = value;
    }

    public IExpression Assignee { get; set; }
    public Token Operator { get; set; }
    public IExpression Value { get; set; }

    public static IExpression ParseAssignmentExpr(Parser parser, IExpression left, Lookups.BindingPower bp)
    {
        throw new NotImplementedException();
    }

    public void Expression()
    {
        throw new NotImplementedException();
    }

    public string Debug()
    {
        throw new NotImplementedException();
    }
}