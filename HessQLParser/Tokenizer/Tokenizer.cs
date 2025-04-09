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
            new RegexPattern(new Regex(@"\("), DefaultHandler(Token.TokenTypes.LEFT_PAREN, "(")),
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

            throw new Exception("Tokenize::Error -> Unexpected Token");
        }
        
        
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
}
