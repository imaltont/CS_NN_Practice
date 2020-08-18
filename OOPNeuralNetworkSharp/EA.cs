using System;
using System.Linq;

namespace OOPNeuralNetworkSharp
{
    public struct IndividualStruct
    {
        public Network Individual { get; set; }
        public double Fitness { get; set; }
        public int FromGeneration { get; set; }
        public int NumMutations { get; set; }

        public IndividualStruct(int numInputs)
        {
            this.Individual = new Network(numInputs);
            this.Fitness = 0;
            this.FromGeneration = 0;
            this.NumMutations = 0;
        }
        public IndividualStruct(Network oldNetwork, int fromGeneration)
        {
            this.Individual = new Network(oldNetwork);
            this.Fitness = 0;
            this.FromGeneration = fromGeneration;
            this.NumMutations = 0;
        }
    }
    public class EA
    {
        public IndividualStruct[] Population { get; private set; }
        public Random RNG { get; private set; }

        public EA(int population, int numInputs, LayerParams[] networkStructure)
        {
            this.Population = new IndividualStruct[population];
            for (int i = 0; i < population; i++)
            {
                Population[i] = new IndividualStruct(numInputs);
                for (int j = 0; j < networkStructure.Length; j++)
                {
                    this.Population[i].Individual.AddLayer(networkStructure[j].F, networkStructure[j].G, networkStructure[j].NumNeurons);
                }
                Population[i].Individual.BuildNetwork();
            }
            this.RNG = new Random();
        }
        private void Mutation(Network network)
        {
            int layer = this.RNG.Next(network.Layers.Length);
            int node = this.RNG.Next(network.Layers[layer].getNeurons().Length);
            int weight = this.RNG.Next(network.Layers[layer].getNeurons()[node].weights.Length);
            network.Layers[layer].getNeurons()[node].weights[weight] = this.RNG.NextDouble() * 2 - 1;

        }
        private Network Crossover(Network parent1, Network parent2, double crossoverRate)
        {
            Network child = new Network(parent1);
            for (int i = 0; i < child.Layers.Length; i++)
            {
                for (int j = 0; j < child.Layers[i].getNeurons().Length; j++)
                {
                    for (int w = 0; w < child.Layers[i].getNeurons()[j].weights.Length; w++)
                    {
                        if (this.RNG.NextDouble() < crossoverRate)
                        {
                            child.Layers[i].getNeurons()[j].weights[w] = parent2.Layers[i].getNeurons()[j].weights[w];
                        }
                    }
                }
            }
            return child;
        }
        private Network TournamentSelection(IndividualStruct[] population, int k, double chanceWin)
        {
            IndividualStruct[] tournament = new IndividualStruct[k];
            for (int i = 0; i < k; i++)
            {
                tournament[i] = population[this.RNG.Next(population.Length)];
            }

            if (chanceWin > this.RNG.NextDouble())
            {
                return tournament[this.RNG.Next(k)].Individual;
            }
            else
            {

                return tournament.OrderBy(f => f.Fitness).ElementAt(0).Individual;
            }
        }
        public void EvolutionaryLoop(DataStruct[] dataset, int generations, int numChildren, double crossoverRate, double mutationRate, int kTournament, double chanceWin)
        {
            for (int i = 0; i < generations; i++)
            {
                double bestFitness = int.MaxValue;
                double averageFitness = 0;
                IndividualStruct[] newPopulation = new IndividualStruct[this.Population.Length];
                for (int j = 0; j < this.Population.Length; j++)
                {
                    this.Population[j].Fitness = Fitness(dataset, this.Population[j]);
                    if (this.Population[j].Fitness < bestFitness)
                    {
                        bestFitness = this.Population[j].Fitness;
                    }
                    averageFitness += this.Population[j].Fitness;
                }
                if (i != generations - 1)
                {
                    var survivors = this.Population.OrderBy(x => x.Fitness);
                    for (int c = 0; c < numChildren; c++)
                    {
                        newPopulation[c] = new IndividualStruct(this.Crossover(this.TournamentSelection(this.Population, kTournament, chanceWin), this.TournamentSelection(this.Population, kTournament, chanceWin), crossoverRate), i + 1);
                    }
                    for (int a = numChildren; a < this.Population.Length; a++)
                    {
                        newPopulation[a] = survivors.ElementAt(a - numChildren);
                    }

                    for (int ind = 0; ind < newPopulation.Length; ind++)
                    {
                        if (this.RNG.NextDouble() < mutationRate)
                        {
                            this.Mutation(newPopulation[ind].Individual);
                            newPopulation[ind].NumMutations += 1;
                        }
                    }
                    this.Population = newPopulation;
                }
                Console.WriteLine($"Generation: {i + 1}");
                Console.WriteLine($"Best fitness: {bestFitness}");
                Console.WriteLine($"Average fitness: {averageFitness / this.Population.Length}");
                Console.WriteLine("-----------------------------------");
            }
        }
        public double Fitness(DataStruct[] dataset, IndividualStruct individual)
        {
            //TODO avoid duplicate code from training
            double accumulatedError = 0;
            foreach (var example in dataset)
            {
                double[] result = individual.Individual.Inference(example.Input);
                double[] delta = individual.Individual.Compare(example.Output, result);

                double[] MSE = new double[delta.Length];
                for (int iDelta = 0; iDelta < delta.Length; iDelta++)
                {
                    MSE[iDelta] = Math.Pow(delta[iDelta], 2);
                }
                accumulatedError += MSE.Sum() / MSE.Length;
            }
            return accumulatedError;

        }
    }
}
