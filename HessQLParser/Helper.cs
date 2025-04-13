using System.Text;
using System.Text.Json;
using HessQLParser.Parser;

namespace HessQLParser;

public class Helper
{
    public static string WriteAst(string input)
    {
        int indentLevel = 0;
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == '{')
            {
                // Ajoute l'accolade ouvrante puis le saut de ligne et augmente l'indentation
                output.Append(c);
                output.AppendLine();
                indentLevel++;
                output.Append(new string(' ', indentLevel * 4));
            }
            else if (c == '}')
            {
                // Termine la ligne en cours pour l'accolade fermante
                output.AppendLine();
                // Réduit l'indentation avant d'afficher l'accolade fermante
                indentLevel = Math.Max(indentLevel - 1, 0);
                output.Append(new string(' ', indentLevel * 4));
                output.Append(c);

                // Si le caractère suivant n'est pas un espace ou un saut de ligne, insère un saut de ligne supplémentaire
                if (i + 1 < input.Length && !char.IsWhiteSpace(input[i + 1]))
                {
                    output.AppendLine();
                    output.Append(new string(' ', indentLevel * 4));
                }
            }
            else
            {
                output.Append(c);
            }
        }
        return output.ToString();
    }

    public static string ConvertToJson(object input)
    {
            
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(input, options);
    }
}