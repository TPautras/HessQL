namespace Sandbox;

public class FileHandling
{
    public static void Main()
    {
        AddRecord("124", "Mercy", "56", "cake.txt");
    }

    private static void AddRecord(string id, string name, string age, string filepath)
    {
        try
        {
            Console.WriteLine("Adding record");
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
            {
                file.WriteLine(id + "," + name + "," + age);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("BUG DANS TON PROGRAMME",ex);
        }
    }
}