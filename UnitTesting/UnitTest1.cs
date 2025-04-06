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
        Console.WriteLine(string.Join(" ",FileHandling.ReadRecord("124","cake.txt",1)));
        Assert.That(string.Join(" ",FileHandling.ReadRecord("124","cake.txt",1)), Is.EqualTo("124 Mercy 56"));
    }

    [Test]
    public void Test2()
    {
        FileHandling.FileStreamTest();
        Assert.Pass();
    }
}