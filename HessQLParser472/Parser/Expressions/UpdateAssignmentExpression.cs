using System;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Expressions
{
    public class UpdateAssignmentExpression : ExpressionEnumerator, IExpression
    {
        public string Column { get; set; }
        public IExpression Expression { get; set; }

        /// <summary>
        /// Constructeur d’UpdateAssignment.
        /// </summary>
        public UpdateAssignmentExpression(string column, IExpression expression)
        {
            Column = column;
            Expression = expression;
        }

        void IExpression.Expression()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Renvoie une chaîne de débuggage.
        /// </summary>
        public string Debug()
        {
            return Column + " = " + Expression.Debug();
        }
    }
}