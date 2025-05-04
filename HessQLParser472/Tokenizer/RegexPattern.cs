using System.Text.RegularExpressions;

namespace HessQLParser
{
    public class RegexPattern
    {
        public Regex Regex;
        public Tokenizer.RegexHandler Handler;

        public RegexPattern(Regex regex, Tokenizer.RegexHandler handler)
        {
            Regex = regex;
            Handler = handler;
        }
    }
}