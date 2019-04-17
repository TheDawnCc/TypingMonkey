using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingMonkey
{
    static class TypingCharacters
    {
        private const string characters = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,./?;'<>:\"[]{}\\|!@#$%^&*()_+~`=-1234567890";

        static TypingCharacters()
        {
            for (int i = 0; i < characters.Length; ++i)
            {
                LegalCharacters.Add(characters[i].ToString());
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0x4e00; i < 0x9fa5; ++i)
            {
                byte[] charChinese = BitConverter.GetBytes(i);
                LegalCharacters.Add(UnicodeEncoding.Unicode.GetString(charChinese).Replace("\0", ""));
            }

            stopwatch.Stop();
            Console.WriteLine($"生成字集时间 : {stopwatch.Elapsed}");
        }

        public static List<string> LegalCharacters { get; } = new List<string>();
    }
}
