namespace Sandbox;

public class ddl
{
    public static void testDB()
    {
        Console.WriteLine("c quoi tu veux le nom");
        string nom = Console.ReadLine();
        HessQLCommands.DDL_Commands.CreateDatabase(nom);
    }

    public static void testTB()
    {
        Console.WriteLine("c quoi tu veux le database");
        string nomDB = Console.ReadLine();
        Console.WriteLine("c quoi tu veux le table");
        string nomTB = Console.ReadLine();
        HessQLCommands.DDL_Commands.CreateTable(nomDB, nomTB);
    }
}