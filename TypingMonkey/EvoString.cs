using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TypingMonkey;
using TypingMonkey.Utility;

namespace TypingMonkey.Entity
{
    /// <summary>
    /// Concrete Prey - in our case is just a common string (fighting for survival!).
    /// </summary>
    class EvoString : APrey<string>
    {
        // This is a typical std::C++ value for maximum random numbers.
        private const double MAX_RAND = 32767.00;

        private List<Func<string, string, string>> LstGenerateAction = new List<Func<string, string, string>>();

        public EvoString(string chromosome)
        {
            this.chromosome = chromosome;

            LstGenerateAction.Add(Generate_Joint);
            LstGenerateAction.Add(Generate_Step);
        }

        /// <summary>
        /// In Nature there's a pseudo-random chance the newborn chromosomes will mutate...
        /// </summary>
        private void Mutate()
        {
            int marker = Dice.Roll(0, this.chromosome.Length - 1);
            int delta = Dice.Roll(0, this.chromosome.Length - marker);

            TypingMonkey monkey = new TypingMonkey();
            string mutatedGenes = monkey.TypeAway(delta);

            char[] genes = this.chromosome.ToCharArray();

            // Override mutated genes.
            for (int i = 0; i < delta; i++)
            {
                genes[marker++] = mutatedGenes[i];
            }

            this.chromosome = new string(genes);
        }

        /// <summary>
        /// 遗传变异
        /// </summary>
        /// <param name="mate"></param>
        /// <returns></returns>
        public override IPrey Mate(IPrey mate)
        {
            string childChromosome = string.Empty;

            EvoString evoStringMate = mate as EvoString;

            if (evoStringMate == null || (this.chromosome.Length != evoStringMate.chromosome.Length))
            {
                throw new Exception("Cross-species mating is not allowed ... yet!");
            }

            // 1. Do chromosome cross-over.
            // 1. 交叉染色体

            childChromosome = GenerateChromosome(this.chromosome, evoStringMate.chromosome);

            // 2. Spawn child.
            // 2. 生成后代
            EvoString child = new EvoString(childChromosome);

            // 3. Roll dice to mutate child.
            // 3. 后代随机突变
            if (Dice.Roll(0, (int)MAX_RAND) > MAX_RAND * MUTATION_RATE)
            {
                child.Mutate();
            }

            return child;
        }

        /// <summary>
        /// 使用随机方法交叉染色体
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="cross"></param>
        /// <returns></returns>
        private string GenerateChromosome(string origin, string cross)
        {
            // 随机采用不同交叉染色体的方法集
            var action = LstGenerateAction[Dice.Roll(0, LstGenerateAction.Count)];

            return action(origin, cross);
        }

        /// <summary>
        /// 交叉染色体方法组--拼接
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="cross"></param>
        /// <returns></returns>
        private string Generate_Joint(string origin, string cross)
        {
            int origineLenght = origin.Length;
            int marker = Dice.Roll(0, origineLenght - 1);
            return origin.Substring(0, marker) + cross.Substring(marker, origineLenght - marker);
        }

        /// <summary>
        /// 交叉染色体方法组--按步长交叉
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="cross"></param>
        /// <returns></returns>
        private string Generate_Step(string origin, string cross)
        {
            int step = Dice.Roll(1, origin.Length - 1);
            var temp = origin.ToCharArray();

            for (int i = 0; i < origin.Length; ++i)
            {
                if (i % step == 0)
                {
                    temp[i] = cross[i];
                }
            }

            return new string(temp);
        }

        /// <summary>
        /// Compares EvoString genetic heritage.
        /// </summary>
        /// <param name="lookAlike"></param>
        /// <returns>Diff between chromosomes, if chromosomes are different in lenght the delta adds up.</returns>
        public int Compare(EvoString lookAlike)
        {
            //return StringDistanceCalculator.Levenshtein(this.chromosome, lookAlike.chromosome);

            return StringDistanceCalculator.iLevenshtein(this.chromosome, lookAlike.chromosome);
        }

        /// <summary>
        /// Serialize EvoString chromosome.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.chromosome;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>Deep-Copy of the object.</returns>
        public override IPrey Clone()
        {
            return new EvoString(this.chromosome);
        }
    }
}
