using HessQLParser;
using static NUnit.Framework.Assert;

namespace UnitTesting.Parser;

public class TokenizerTest
{
    [Test]
    public void TestTokenizeSelect()
    {
        const string source = "SELECT * FROM";
        var tokens = Tokenizer.Tokenize(source);
        That(tokens, Is.Not.Null);
        That(Token.Debug(tokens?[0] ?? throw new InvalidOperationException()), Is.EqualTo("SELECT"));
    }
}