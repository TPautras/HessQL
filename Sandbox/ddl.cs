namespace Sandbox;

public class ddl
{
    public static void testDB()
    {
        Console.WriteLine("c quoi tu veux le nom");
        string nom = Console.ReadLine();
        HessQLCommands.DDL_Commands.Create.CreateDatabase(nom);
    }

    public static void testTB()
    {
        Console.WriteLine("c quoi tu veux le database");
        string nomDB = Console.ReadLine();
        Console.WriteLine("c quoi tu veux le table");
        string nomTB = Console.ReadLine();
        HessQLCommands.DDL_Commands.Create.CreateTable(nomDB, nomTB);
    }

    public static void testAlterADD()
    {
        Console.WriteLine("c quoi tu veux le nom");
        string nomDB = Console.ReadLine();
        Console.WriteLine("c quoi tu veux le table");
        string nomTB = Console.ReadLine();
        Console.WriteLine("c quoi tu veux le colonne");
        string nomCL = Console.ReadLine();
        HessQLCommands.DDL_Commands.Alter.Alter_Add(nomDB, nomTB, nomCL);
    }
}