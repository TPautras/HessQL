namespace UnitTesting.Commands.DDL;

[TestFixture]
public class CreateDBtest
{
    [Test]
    public static void test1()
    {
        string nom = "ouiouaa";
        HessQLCommands.DDL_Commands.Create.CreateTable(nom, nom);
        
    }
  
}