using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Sandbox
{
    public class FileHandling2
    {
        static Memory<byte>[] indexes = new Memory<byte>[10];
        struct IndexEntry { public int Index; public int Length; public IntPtr Pointer; }
        static IndexEntry[] pointerTable = new IndexEntry[10];

        static void GenererFichier(string filePath)
        {
            byte[] content = Encoding.UTF8.GetBytes("Le chat dort sur le canapé et regarde par la fenêtre en toute tranquillité");
            File.WriteAllBytes(filePath, content);
        }

        static void EnregistrerIndexes(string filePath)
        {
            byte[] content = File.ReadAllBytes(filePath);
            int start = -1, count = 0;
            for (int i = 0; i < content.Length && count < 10; i++)
            {
                if (content[i] != 32 && content[i] != 9 && content[i] != 10 && content[i] != 13)
                {
                    if (start == -1) start = i;
                }
                else
                {
                    if (start != -1)
                    {
                        int len = i - start;
                        indexes[count++] = new Memory<byte>(content, start, len);
                        start = -1;
                    }
                }
            }
            if (start != -1 && count < 10)
            {
                int len = content.Length - start;
                indexes[count] = new Memory<byte>(content, start, len);
            }
        }

        static void CreerEtEnregistrerLaPointerTable(string indexFilePath)
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i].Length > 0)
                {
                    byte[] data = indexes[i].ToArray();
                    int len = data.Length;
                    IntPtr ptr = Marshal.AllocHGlobal(len);
                    Marshal.Copy(data, 0, ptr, len);
                    pointerTable[i] = new IndexEntry { Index = i, Length = len, Pointer = ptr };
                }
            }
            using (FileStream fs = new FileStream(indexFilePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                for (int i = 0; i < pointerTable.Length; i++)
                {
                    bw.Write(pointerTable[i].Index);
                    bw.Write(pointerTable[i].Length);
                    bw.Write(pointerTable[i].Pointer.ToInt64());
                }
            }
        }

        static Memory<byte> Recherche(int indice)
        {
            if (indice < 0 || indice >= pointerTable.Length) return Memory<byte>.Empty;
            var entry = pointerTable[indice];
            byte[] data = new byte[entry.Length];
            Marshal.Copy(entry.Pointer, data, 0, entry.Length);
            return new Memory<byte>(data);
        }

        public static void Main22()
        {
            string filePath = "fichier.bin";
            string indexFilePath = "indexes.bin";
            GenererFichier(filePath);
            EnregistrerIndexes(filePath);
            CreerEtEnregistrerLaPointerTable(indexFilePath);
            Memory<byte> res = Recherche(3);
            Console.WriteLine(Encoding.UTF8.GetString(res.Span));
            for (int i = 0; i < 10; i++)
            {
                Memory<byte> r = Recherche(i);
                Console.WriteLine(Encoding.UTF8.GetString(r.Span));
            }
            for (int i = 0; i < pointerTable.Length; i++)
            {
                if (pointerTable[i].Pointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(pointerTable[i].Pointer);
            }
        }
    }
}
