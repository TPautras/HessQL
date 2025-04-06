using NUnit.Framework.Internal.Execution;
using Sandbox;

namespace UnitTesting;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        FileHandling.Main();
        Assert.Pass();
    }
}