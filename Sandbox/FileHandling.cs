﻿using System.Net;
using System.Text;
using System.Text.Json;
using HessQLParser;
using HessQLParser.Parser;
using HessQLParser.Parser.Statements;

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
            using (StreamWriter sr = File.AppendText(@filepath))
            {
                sr.WriteLine(id + "," + name + "," + age);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("BUG DANS TON PROGRAMME",ex);
        }
    }

    public static void FileStreamTest()
    {
        FileStream fs = new FileStream("TestFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        string test = "Bonjour monde";
        byte[] writeArr = Encoding.UTF8.GetBytes(test);
        fs.Write(writeArr, 0, writeArr.Length);
        fs.Close();
    } 

    public static string[] ReadRecord(string searchTerm, string filepath, int positionOfSearchTerm)
    {
        positionOfSearchTerm--;
        string[] recordNotFound = { "Record not found" };
        try
        {
            string[] lines = File.ReadAllLines(@filepath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');
                if (recordMatches(searchTerm, line, positionOfSearchTerm))
                {
                    Console.WriteLine("Found record");
                    return line;
                }
            }
            return recordNotFound;
        }
        catch (Exception e)
        {
            Console.WriteLine("BUG DANS TON PROGRAMME");
            throw new ApplicationException("BUG DANS TON PROGRAMME", e);
        }
    }

    private static bool recordMatches(string searchTerm, string[] line, int positionOfSearchTerm)
    {
        if (line[positionOfSearchTerm].Equals(searchTerm))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Test()
    {
        var source = "12 + 15 * '44'-44+1212+44-112;";
        source += "DECLARE bonjour SET 40";
        var tokens = Tokenizer.Tokenize(source);
        if (tokens != null)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine(Token.Debug(token));
            }

            // Sérialisation JSON
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(tokens, options);
                File.WriteAllText("tokens.json", json);
                Console.WriteLine("Les tokens ont été enregistrés dans tokens.json");
                var test = Parser.Parse(tokens);
                string formatted = Helper.WriteAst(test.Debug());
                Console.WriteLine(formatted);
                File.WriteAllText("parserOutput.txt", formatted);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public static void Test2()
    {
        const string source = "SELECT";
        var tokens = Tokenizer.Tokenize(source);
        BlockStatement parsedStatement = Parser.Parse(tokens);
        Console.WriteLine(parsedStatement.Debug());
    }
}