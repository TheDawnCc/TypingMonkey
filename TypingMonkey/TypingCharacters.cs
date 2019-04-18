using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingMonkey
{
    static class TypingCharacters
    {
        private const string defaultStr = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,./?;'<>:\"[]{}\\|!@#$%^&*()_+~`=-1234567890";
        private static List<string> chineseStr = new List<string>();

        static TypingCharacters()
        {
            for (int i = 0; i < defaultStr.Length; ++i)
            {
                LegalCharacters.Add(defaultStr[i].ToString());
            }


            for (int i = 0x4e00; i < 0x9fa5; ++i)
            {
                byte[] charChinese = BitConverter.GetBytes(i);
                chineseStr.Add(UnicodeEncoding.Unicode.GetString(charChinese).Replace("\0", ""));
            }
        }

        public static void GetCharacterSet(string source)
        {
            bool isChinese = false;

            for (int i = 0; i < source.Length; ++i)
            {
                if (chineseStr.Contains(source[i].ToString()))
                {
                    isChinese = true;
                    break;
                }
            }

            if (isChinese)
            {
                LegalCharacters.AddRange(chineseStr);
            }
            else
            {
                LegalCharacters.Clear();

                for (int i = 0; i < defaultStr.Length; ++i)
                {
                    LegalCharacters.Add(defaultStr[i].ToString());
                }
            }
        }

        public static List<string> LegalCharacters { get; } = new List<string>();

        public static Dictionary<string, int> DicCharacters { get; } = new Dictionary<string, int>();
    }
}
