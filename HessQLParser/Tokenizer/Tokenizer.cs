using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HessQLParser;

public partial class Tokenizer
{
    public List<RegexPattern> RegexPatterns = new List<RegexPattern>();
    public List<Token>? Tokens;
    public string Source;
    public int Position;

    public Tokenizer(List<Token> tokens, string source, int position)
    {
        Tokens = tokens;
        this.Source = source;
        this.Position = position;
    }
    private Tokenizer(string source)
    {
        this.Source = source;
        this.Position = 0;
        this.Tokens = new List<Token>();
        this.RegexPatterns = new List<RegexPattern>
        {
            new RegexPattern(new Regex(@"[0-9]+(\.[0-9]+)?"), NumericHandler()),
            new RegexPattern(new Regex(@"\s+"), SkipHandler()),
            new RegexPattern(new Regex(@"'[^']*'"), StringHandler()),
            new RegexPattern(new Regex(@"#.*"), CommentHandler()),
            new RegexPattern(new Regex(@"[a-zA-Z][a-zA-Z0-9_]*"), SymbolHandler()),
            new RegexPattern(new Regex(@"FOR XML"), DefaultHandler(Token.TokenTypes.FOR_XML, "FOR XML")),
            new RegexPattern(new Regex(@"FOR JSON"), DefaultHandler(Token.TokenTypes.FOR_JSON, "FOR JSON")),
            new RegexPattern(new Regex(@"OPENJSON"), DefaultHandler(Token.TokenTypes.OPEN_JSON, "OPENJSON")),
            new RegexPattern(new Regex(@"\+"), DefaultHandler(Token.TokenTypes.PLUS, "+")),
            new RegexPattern(new Regex(@"-"), DefaultHandler(Token.TokenTypes.MINUS, "-")),
            new RegexPattern(new Regex(@"\*"), DefaultHandler(Token.TokenTypes.ASTERISK, "*")),
            new RegexPattern(new Regex(@"/"), DefaultHandler(Token.TokenTypes.SLASH, "/")),
            new RegexPattern(new Regex(@"%"), DefaultHandler(Token.TokenTypes.PERCENT, "%")),
            new RegexPattern(new Regex(@"="), DefaultHandler(Token.TokenTypes.EQUALS, "=")),
            new RegexPattern(new Regex(@"!="), DefaultHandler(Token.TokenTypes.NOT_EQUALS, "!=")),
            new RegexPattern(new Regex(@">"), DefaultHandler(Token.TokenTypes.GREATER_THAN, ">")),
            new RegexPattern(new Regex(@"<"), DefaultHandler(Token.TokenTypes.LESS_THAN, "<")),
            new RegexPattern(new Regex(@">="), DefaultHandler(Token.TokenTypes.GREATER_THAN_OR_EQUAL, ">=")),
            new RegexPattern(new Regex(@"<="), DefaultHandler(Token.TokenTypes.LESS_THAN_OR_EQUAL, "<=")),
            new RegexPattern(new Regex(@"\("), DefaultHandler(Token.TokenTypes.LEFT_PAREN, "(")),
            new RegexPattern(new Regex(@"\)"), DefaultHandler(Token.TokenTypes.RIGHT_PAREN, ")")),
            new RegexPattern(new Regex(@","), DefaultHandler(Token.TokenTypes.COMMA, ",")),
            new RegexPattern(new Regex(@";"), DefaultHandler(Token.TokenTypes.SEMICOLON, ";"))
        };
    }

    public delegate void RegexHandler(Tokenizer tokenizer, Match match);
    
    private void AdvanceN(int n)
    {
        this.Position += n;
    }

    private void Push(Token token)
    {
        this.Tokens?.Add(token);
    }

    private byte At()
    {
        return (byte)this.Source[this.Position];
    }

    private string Remainder()
    {
        return this.Source.Substring(this.Position);
    }

    private bool AtEnd()
    {
        return this.Position >= this.Source.Length;
    }
    
    public static List<Token>? Tokenize(string source)
    {
        Tokenizer tokenizer = new Tokenizer(source);

        while (!tokenizer.AtEnd())
        {
            bool matched = false;

            foreach (RegexPattern regexPattern in tokenizer.RegexPatterns)
            {
                var match = regexPattern.Regex.Match(tokenizer.Remainder());
                int? startIndex = null;
                int? endIndex = null;
                if (match.Success)
                {
                    startIndex = match.Index;
                    endIndex = match.Index + match.Length;
                }

                var loc = new int?[] { startIndex, endIndex };
                
                if (loc.Length != 0 && loc[0] == 0)
                {
                    regexPattern.Handler(tokenizer, match);
                    matched = true;
                    break;
                }
            }
            
            if (!matched)
                throw new Exception("Tokenize::Error -> Unexpected Token"); //TODO : Ajouter la localisation de l'erreur et un type d'erreur specifique
        }
        tokenizer.Push(Token.NewToken("END OF FILE", Token.TokenTypes.END_OF_FILE));
        return tokenizer.Tokens;
    }
    
    private static RegexHandler DefaultHandler(Token.TokenTypes kind, string value)
    {
        return (Tokenizer tokenizer, Match _) =>
        {
            tokenizer.AdvanceN(value.Length);
            tokenizer.Push(Token.NewToken(value, kind));
        };
    }
    
    private static RegexHandler NumericHandler()
    {
        
        return (Tokenizer tokenizer, Match match ) =>
        {
            string matchedValue = match.Value;
            tokenizer.Push(Token.NewToken( matchedValue, Token.TokenTypes.INTEGER_LITERAL));
            tokenizer.AdvanceN(matchedValue.Length);
        };
    }
    
    private static RegexHandler StringHandler()
    {
        
        return (Tokenizer tokenizer, Match match ) =>
        {
            string matchedValue = match.Value;
            string value = matchedValue.Substring(1, matchedValue.Length - 2);
            tokenizer.Push(Token.NewToken( value, Token.TokenTypes.STRING_LITERAL));
            tokenizer.AdvanceN(matchedValue.Length);
        };
    }
    
    private static RegexHandler CommentHandler()
    {
        
        return (Tokenizer tokenizer, Match match ) =>
        {
            string matchedValue = match.Value;
            string value = matchedValue.Substring(1);
            tokenizer.Push(Token.NewToken( value, Token.TokenTypes.COMMENT_SINGLE_LINE));
            tokenizer.AdvanceN(matchedValue.Length);
        };
    }

    private static RegexHandler SkipHandler()
    {
        
        return (Tokenizer tokenizer, Match match ) =>
        {
            tokenizer.AdvanceN(match.Value.Length);
        };
    }
    
    private static RegexHandler SymbolHandler()
    {
        
        return (Tokenizer tokenizer, Match match ) =>
        {
            string matchedValue = match.Value;
            if(Token.TokenKindToStringMap.ContainsValue(matchedValue))
            {
                var myKey = Token.TokenKindToStringMap.FirstOrDefault(x => x.Value == matchedValue).Key;
                tokenizer.Push(Token.NewToken(matchedValue, myKey));
            }
            else
            {
                tokenizer.Push(Token.NewToken(matchedValue, Token.TokenTypes.IDENTIFIER));
            }

            tokenizer.AdvanceN(match.Value.Length);
        };
    }
}
