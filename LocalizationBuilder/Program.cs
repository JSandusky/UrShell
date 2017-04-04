using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationBuilder
{
    /// <summary>
    /// Scans *.cs files looking for Strings that contains ".Localize() then identifying the string in the quotes
    /// ie. extract "My String".Localize() into "My String" without the quotes
    /// Writes that to a file to use for a base file for localization (easily convertible into anything else)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Localization Builder (C) 2015 JSandusky");
                Console.WriteLine("    Generates a strings dump of strings to localize");
                Console.WriteLine("    Only able to identify explicit strings");
                Console.WriteLine("Usage: LocalizationBuilder <path>");
                return;
            }
            string path = args[0];

            List<string> written = new List<string>();

            StringBuilder output = new StringBuilder();

            output.AppendLine("Insert Language Name Here");
            processDirectory(path, output, written);

            System.IO.File.WriteAllText("Strings", output.ToString());
        }

        static void processDirectory(string path, StringBuilder output, List<string> written)
        {
            foreach (string file in System.IO.Directory.EnumerateFiles(path))
            {
                if (System.IO.Path.GetExtension(file).Equals(".cs"))
                {
                    string code = System.IO.File.ReadAllText(file);
                    int curIndex = code.IndexOf("\".Localize()");
                    while (curIndex != -1)
                    {
                        int startWord = curIndex - 1;
                        char cCode = code[startWord];
                        while (cCode != '"')
                        {
                            startWord--;
                            cCode = code[startWord];
                        }

                        string extractedPhrase = code.Substring(startWord + 1, curIndex - startWord - 1);
                        if (!written.Contains(extractedPhrase))
                        {
                            written.Add(extractedPhrase);
                            output.AppendFormat("{0}\t\r\n", extractedPhrase);
                        }

                        curIndex = code.IndexOf("\".Localize()", curIndex + 1);
                    }
                }
            }

            foreach (string dir in System.IO.Directory.EnumerateDirectories(path))
                processDirectory(dir, output, written);
        }
    }
}
