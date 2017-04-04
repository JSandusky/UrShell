using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorCore;
using Sce.Atf;

namespace EditorWinForms
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string input)
        {
            StringBuilder sb = new StringBuilder();
            int lastCap = -1; //-1 means not a letter, 0 == lowercase, 1 == uppercase
            for (int i = 0; i < input.Length; ++i)
            {
                if (Char.IsLetter(input[i]))
                {
                    if (Char.IsLower(input[i]))
                    {
                        lastCap = 0;
                        sb.Append(input[i]);
                    }
                    else
                    {
                        // Insert a space if we've arrived from a lowercase letter or a number, 
                        // ie. UsingAtf == Using Atf
                        // ie. UsingATF == Using ATF
                        // ie. UsingATFForUI == Using ATF For UI
                        if (lastCap != 1 && sb.Length > 0)
                            sb.Append(' ');
                        // or if we're a capital about to hit a space after a capital
                        else if (i > 0 && i < input.Length - 1 && Char.IsLetter(input[i + 1]) && Char.IsLower(input[i + 1]) && lastCap == 1)
                            sb.Append(' ');
                        sb.Append(input[i]);
                        lastCap = 1;
                    }
                }
                else
                {
                    if (lastCap != -1)
                        sb.Append(' ');
                    sb.Append(input[i]);
                    lastCap = -1;
                }
            }
            return sb.ToString().Localize();
        }

        public static string Extract(this string str, char open, char close)
        {
            int startidx = str.IndexOf(open);
            int endidx = str.IndexOf(close);
            if (startidx < endidx)
                return str.Substring(startidx + 1, endidx - startidx - 1);
            return null;
        }

        public static string[] SubArray(this string[] data, int index, int length)
        {
            string[] result = new string[length];
            System.Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static string ToSpaceQuoted(this string str)
        {
            if (str.Contains(' '))
                return String.Format("\"{0}\"", str);
            return str;
        }

        public static string ToFileSize(int value)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = value;
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
