namespace HessQLCommands.DDL_Commands;

public class Create
{
    public static void CreateDatabase(string NomDB)
    {
        string CheminDB = Path.Combine(Directory.GetCurrentDirectory(), NomDB);

        if (!Directory.Exists(CheminDB))
        {
            Directory.CreateDirectory(CheminDB);
            string FichierDB = Path.Combine(CheminDB, NomDB + "_Description.csv");
            File.Create(FichierDB);
        }
        else
        {
            Console.WriteLine("La base de donnees" + NomDB + " existe deja.");
        }

    }

    public static void CreateTable(string NomDB, string NomTB)
    {
        string CheminDB = Path.Combine(Directory.GetCurrentDirectory(), NomDB);
        
        if (!Directory.Exists(CheminDB))
        {
            Console.WriteLine("La base de donnees " + NomDB + " n'existe pas");
        }
        else
        {
            string DossierDB = Path.Combine(CheminDB, NomTB);
            string FichierTB = Path.Combine(DossierDB, NomTB+"_Peuplement.csv");
            string Fichier_DescriptionTB = Path.Combine(DossierDB, NomTB+"_Description.csv");
            string Fichier_DescriptionDB = Path.Combine(CheminDB, NomDB+"_Description.csv");

            if (!Directory.Exists(DossierDB))
            {
                Directory.CreateDirectory(DossierDB);
            }

            if (!File.Exists(FichierTB))
            {
                File.Create(FichierTB);
                File.Create(Fichier_DescriptionTB);
                try
                {
                    using (StreamWriter sw = File.AppendText(Fichier_DescriptionDB))
                    {
                        sw.WriteLine(NomTB);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("la table"+ NomTB + " existe deja.");
            }
        }
    }
}