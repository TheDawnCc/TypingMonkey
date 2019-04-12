using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypingMonkey.Entity
{
    /// <summary>
    ///  This method is very powerful combined with Interfaces.
    /// </summary>
    abstract class APrey<T>:IPrey
    {
        /// <summary>
        /// MUTATION_RATE is common to all our possible preys.
        /// 突变率
        /// </summary>
        protected const double MUTATION_RATE = 0.25;

        /// <summary>
        /// 染色体
        /// </summary>
        protected T chromosome;

        /// <summary>
        /// Generic method to reset prey chromosome.
        /// 重置染色体的通用方法
        /// </summary>
        /// <param name="newChromosome">New Generic Chromosome.</param>
        public void ResetChromosome(T newChromosome)
        {
            this.chromosome = newChromosome;
        }

        #region IPrey Members

        abstract public IPrey Mate(IPrey mate);

        abstract public IPrey Clone();

        #endregion
    }
}
