using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TypingMonkey
{
    /// <summary>
    /// The Typing monkey generates random strings - can't be static 'cause it's a monkey.
    /// </summary>
    /// <remarks>
    /// If you wait long enough it will eventually produce Shakespeare.
    /// </remarks>
    class TypingMonkey
    {
        //private const string legalCharacters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890.";
        //private const string legalCharacters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,./?;'<>:\"[]{}\\|!@#$%^&*()_+~`=-1234567890";

        //private const string characters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,./?;'<>:\"[]{}\\|!@#$%^&*()_+~`=-1234567890";
        //private List<string> legalCharacters = new List<string>();

        static Random random = new Random();



        /// <summary>
        /// <para>The Typing Monkey Generates a random string with the given length.</para>
        /// <para>生成一串随机字符串</para>
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <returns>Random string</returns>
        public string TypeAway(int size)
        {
            StringBuilder builder = new StringBuilder();
            string ch;

            for (int i = 0; i < size; i++)
            {
                //ch = legalCharacters[random.Next(0, legalCharacters.Count)];
                ch = TypingCharacters.LegalCharacters[random.Next(0, TypingCharacters.LegalCharacters.Count)];
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
