using HessQLParser;

namespace UnitTesting.Parser;

public class TokensTests
{
    [Test]
    public void Test_Init()
    {
        Token myTokenEof = Token.NewToken("", Token.TokenTypes.END_OF_FILE);
        Assert.That(myTokenEof.TokenType, Is.EqualTo(Token.TokenTypes.END_OF_FILE));
    }

    [Test]
    public void Test_Debug()
    {
        Token myToken = Token.NewToken("test", Token.TokenTypes.STRING_LITERAL);
        Token myTokenEof = Token.NewToken("", Token.TokenTypes.END_OF_FILE);
        Assert.That(Token.Debug(myToken), Is.EqualTo("string: test"));
        Assert.That(Token.Debug(myTokenEof), Is.EqualTo("END OF FILE"));
    }
}