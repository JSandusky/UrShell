﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingGenerator
{
    public static class StringUtils
    {
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
    }
}