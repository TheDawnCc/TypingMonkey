﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TypingMonkey.Utility
{
    /// <summary>
    /// <para>计算两个字符串的编辑距离</para>
    /// <para>(将一个单词转变为另一个单词所需单字符的插入,删除,替换的最小数量)</para>
    /// </summary>
    static class StringDistanceCalculator
    {

        private static readonly object __lock = new object();
        /// <summary>
        /// Calculates Levenshtein distance between strings.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Int32 Levenshtein(String a, String b)
        {
            //TODO:需要优化
            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            int value;
            if (TypingCharacters.DicCharacters.TryGetValue(b, out value))
            {
                return value;
            }

            Int32 cost;
            Int32[,] d = new int[a.Length + 1, b.Length + 1];
            Int32 min1;
            Int32 min2;
            Int32 min3;

            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a.Substring(i - 1, 1) == b.Substring(j - 1, 1)));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            lock (__lock)
            {
                TypingCharacters.DicCharacters.Add(b, d[d.GetUpperBound(0), d.GetUpperBound(1)]);
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }
    }
}
