﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using TypingMonkey.Entity;
using TypingMonkey.Utility;

namespace TypingMonkey.Control
{
    /// <summary>
    /// Manages Evolution and exposes control methods.
    /// </summary>
    class EvolutionManager
    {
        /// <summary>
        /// A bunch of constants.
        /// </summary>
        private int MAX_ITER = 2000;
        private const double ELITISM_RATE = 0.1;

        /// <summary>
        /// 世代
        /// </summary>
        private int generationsCount = 0;

        /// <summary>
        /// Our population of EvoStrings.
        /// </summary>
        private List<IPrey> population = null;

        /// <summary>
        /// Evolution Target.
        /// </summary>
        private IPrey target = null;

        /// <summary>
        /// Our predator - will evaluate fitness of preys.
        /// </summary>
        private IPredator predator = null;

        /// <summary>
        /// Evolution Manager Only Constructor to ensure correct instantiation.
        /// </summary>
        /// <param name="populationSize"></param>
        /// <param name="target">Evolution target.</param>
        public EvolutionManager(int populationSize, string target)
        {
            this.SetupEvolution(populationSize, target);
        }

        
        private void SetupEvolution(int populationSize, string target)
        {
            target = StringHelper.ClearRedundantChar(target);

            this.target = PreyFactory.CreateEvoStringPrey(target);
            this.predator = PredatorFactory.CreateEvoStringPredator(this.target as EvoString);

            // 判重字典
            TypingCharacters.DicCharacters.Clear();
            TypingCharacters.DicCharacters.Add(target, 0);

            this.population = new List<IPrey>(populationSize);

            // Let the Typing Monkey in the factory type away.
            for (int i = 0; i < populationSize; i++)
            {
                this.population.Add(PreyFactory.CreateRandomEvoStringPrey(target.Length));
            }
        }

        /// <summary>
        /// Evolve Generation and return most Fit.
        /// </summary>
        /// <returns>Returns mostFit - null if MAXIMUM number of Iterations has been reached.</returns>
        public IPrey EvolveGeneration()
        {
            // 按照字符串距离进行排序
            this.population = this.population.AsParallel().OrderBy(each => this.predator.EvaluateFitness(each)).ToList();


            IPrey mostFit = null;

            if (this.generationsCount < MAX_ITER)
            {
                // 1. Retain Most Fitbased on Elitism rate - the king of the jungle will survive more generations!
                // 1. 维持最符合的种群后代
                int eliteSize = Convert.ToInt32(this.population.Count * ELITISM_RATE);
                List<IPrey> buffer = this.CopyEliteMembers(this.population.GetRange(0, eliteSize));

                // 2. Mate The rest.
                // 2. 对剩余的进行遗传变异
                for (int i = eliteSize; i < this.population.Count; i++)
                {
                    int activeMateIndex = Dice.Roll(0, this.population.Count - 1);
                    int passiveMateIndex = Dice.Roll(0, this.population.Count - 1);

                    // 进行遗传变异
                    IPrey child = this.population[activeMateIndex].Mate(this.population[passiveMateIndex]);

                    buffer.Add(child);
                }

                this.population = buffer;
              
                this.population = this.population.AsParallel().OrderBy(each => this.predator.EvaluateFitness(each)).ToList();

                // Get Most Fit
                mostFit = this.population[0];

                //Increase Iteration Count
                this.generationsCount++;
            }

            return mostFit;
        }

        private List<IPrey> CopyEliteMembers(List<IPrey> elite)
        {
            List<IPrey> eliteDeepCopy = new List<IPrey>();  
            
            foreach (IPrey prey in elite)
            {
                eliteDeepCopy.Add(prey.Clone());
            }

            return eliteDeepCopy;
        }

        /// <summary>
        /// Get Most Fit at any moment.
        /// </summary>
        /// <returns></returns>
        public IPrey GetMostFit()
        {
            // Order population by Fitness.
            this.population = this.population.AsParallel().OrderBy(each => this.predator.EvaluateFitness(each)).ToList();

            return this.population[0];
        }

        /// <summary>
        /// <para>Get or Set Maximu Number Of Iterations.</para>
        /// <para>获取或者设置迭代最大次数</para>
        /// </summary>
        public int MaxIterations
        {
            set 
            {
                this.MAX_ITER = value;
            }

            get
            {
                return this.MAX_ITER;
            }
        }

        /// <summary>
        /// 迭代次数
        /// </summary>
        public int IterationCount
        {
            get 
            {
                return this.generationsCount;
            }
        }
    }
}
