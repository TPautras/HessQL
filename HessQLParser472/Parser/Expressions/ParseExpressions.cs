using System;
using HessQLParser.Errors;
namespace HessQLParser.Parser.Expressions
{
    public class ParseExpressions
    {
        public static IExpression ParseExpression(Parser parser, Lookups.BindingPower bp)
        {
            if (parser.CurrentTokenKind() == Token.TokenTypes.END_OF_FILE)
                throw new ParsingException(ParserErrors.UnexpectedEOF);
            Token.TokenTypes tokenType = parser.CurrentTokenKind();
            if (!Lookups.NudLookup.TryGetValue(tokenType, out var nudFn))
                throw new ParsingException(string.Format(ParserErrors.NudNotFound, tokenType));
            IExpression left = nudFn(parser);
            while (Lookups.BpLookup.TryGetValue(parser.CurrentTokenKind(), out var currentBp) && bp < currentBp)
            {
                tokenType = parser.CurrentTokenKind();
                if (!Lookups.LedLookup.TryGetValue(tokenType, out var ledFn))
                    throw new ParsingException(string.Format(ParserErrors.LedNotFound, tokenType));
                left = ledFn(parser, left, currentBp);
            }
            return left;
        }

        public static IExpression ParsePrimaryExpr(Parser parser)
        {
            switch (parser.CurrentTokenKind())
            {
                case Token.TokenTypes.INTEGER_LITERAL:
                    var number = Int32.Parse(parser.Advance().Value);
                    return new NumberExpression() { Value = number };
                case Token.TokenTypes.REAL_LITERAL:
                    float realNumber = float.Parse(parser.Advance().Value);
                    return new NumberExpression() { Value = realNumber };
                case Token.TokenTypes.STRING_LITERAL:
                    return new StringExpression() { Value = parser.Advance().Value };
                case Token.TokenTypes.IDENTIFIER:
                    return new SymbolExpression() { Value = parser.Advance().Value };
                default:
                    throw new ParsingException("Expression primaire inattendue");
            }
        }

        public static IExpression ParseBinaryExpression(Parser parser, IExpression left, Lookups.BindingPower bp)
        {
            Token operatorToken = parser.Advance();
            IExpression right = ParseExpression(parser, bp);
            return new BinaryExpression(left, operatorToken.TokenType, right);
        }
    }
}
