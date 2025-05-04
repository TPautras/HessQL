using System;
using HessQLParser.Parser.CustomEnumerators;
using HessQLParser.Parser.Expressions;

namespace HessQLParser.Parser.Statements
{
    public class VarDeclStmt : IStatement
    {
        public string VarName;
        public  IExpression AssignedValue;

        public VarDeclStmt(string varName, IExpression assignedValue = null)
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
            res += "VariableName{ \"" + VarName + "\"}Assigned Value{";
            if (AssignedValue != null) res += AssignedValue.Debug() + "}";
            return res+"}";
        }

        public static IStatement ParseVarDeclStmt(Parser parser)
        {
            parser.Advance();
            string varName = parser.ExpectError(Token.TokenTypes.IDENTIFIER,"Expected to find variable name after a variable declaration statement").Value;
            IExpression assignedValue = null;
            if (parser.CurrentTokenKind() == Token.TokenTypes.ASSIGNMENT)
            {
                parser.Expect(Token.TokenTypes.ASSIGNMENT);
                assignedValue = ParseExpressions.ParseExpression(parser, Lookups.BindingPower.Lowest);
            }
            else
            {
                parser.Expect(Token.TokenTypes.SEMICOLON);
            }
        
            return new VarDeclStmt(
                varName,
                assignedValue
            );
        }
    }
}