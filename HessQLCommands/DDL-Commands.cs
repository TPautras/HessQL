namespace HessQLCommands;

public class DDL_Commands
{
    public static void CreateDatabase(string NomDB)
    {
        string CheminDB = Path.Combine(Directory.GetCurrentDirectory(), NomDB);

        if (!Directory.Exists(CheminDB))
        {
            Directory.CreateDirectory(CheminDB);
        }
        else
        {
            Console.WriteLine("CA MARCHE PAAAA");
        }

    }
}