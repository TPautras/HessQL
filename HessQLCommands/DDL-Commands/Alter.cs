namespace HessQLCommands.DDL_Commands;

public class Alter
{
    public static void Alter_Add(string NomDB, string NomTB, string NomCL)
    {
        string CheminDB = Path.Combine(Directory.GetCurrentDirectory(), NomDB);
        string DossierTB = Path.Combine(CheminDB, NomTB);
        string Fichier_DescriptionTB = Path.Combine(DossierTB, NomTB + "_Description.csv");
        string Fichier_DescriptionDB = Path.Combine(CheminDB, NomDB + "_Description.csv");

        if (!Directory.Exists(CheminDB))
        {
            Console.WriteLine("DB existe pas");
            return;
        }

        if (!Directory.Exists(DossierTB))
        {
            Console.WriteLine("TB existe pas");
            return;
        }

        if (!File.Exists(Fichier_DescriptionTB))
        {
            Console.WriteLine("Fichier_DescriptionTB existe pas");
            return;
        }

        if (!File.Exists(Fichier_DescriptionDB))
        {
            Console.WriteLine("Fichier_DescriptionDB existe pas");
            return;
        }

        try
        {
            List<string> lignesTB = File.ReadAllLines(Fichier_DescriptionTB).ToList();
            List<string> lignesDB = File.ReadAllLines(Fichier_DescriptionDB).ToList();

            if (lignesTB.Count == 0)
            {
                lignesTB.Add(NomCL);
            }
            else
            {
                string[] colonnes = lignesTB[0].Split(',');

                if (colonnes.Contains(NomCL))
                {
                    Console.WriteLine("Cette attibut existe deja");
                    return;
                }
                lignesTB[0] = "," + NomCL;
            }
            
            File.WriteAllLines(Fichier_DescriptionTB, lignesTB);
            
            bool modif = false;
            int i = 0;
            
            while (modif == false)
            {
                string[] attributs = lignesDB[i].Split(',');

                if (attributs[0] == NomTB)
                {
                    if (attributs.Contains(NomCL))
                    {
                        Console.WriteLine("Cette attibut existe deja");
                        return;
                    }
                    else
                    {
                        lignesDB[i] += "," + NomCL;
                        modif = true;
                    }
                }
                i++;
            }

            if (modif == false)
            {
                lignesDB.Add(NomTB+"," + NomCL);
            }
            
            File.WriteAllLines(Fichier_DescriptionDB, lignesDB);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}