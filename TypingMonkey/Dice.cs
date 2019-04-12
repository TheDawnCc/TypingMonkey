using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingMonkey.Utility
{
    /// <summary>
    /// Dice class to get random numbers.
    /// </summary>
    static class Dice
    {
        private static Random random = new Random();

        /// <summary>
        /// <para>Roll method to generate a sparkle of chance with a random number in a range.</para>
        /// <para>生成一个范围内的随机数</para>
        /// </summary>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        /// <returns></returns>
        public static int Roll(int min, int max)
        {
            return random.Next(min, max);
        }

        /// <summary>
        /// <para>Assumes we are rolling two standard 6 faces dice. </para>
        /// <para>假设扔2个6面骰子</para>
        /// </summary>
        /// <returns>A number between 2 and 12</returns>
        public static int Roll()
        {
            return Roll(2, 12);
        }
    }
}
