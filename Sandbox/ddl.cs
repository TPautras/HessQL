namespace Sandbox;

public class ddl
{
    public static void GAMBERGE()
    {
        Console.WriteLine("c quoi tu veux le nom");
        string nom = Console.ReadLine();
        HessQLCommands.DDL_Commands.CreateDatabase(nom);
    }
}