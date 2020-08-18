//TODO: Fix weight update so it learns more than output 50/50 every time
using System;
using System.Linq;

namespace OOPNeuralNetworkSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Activation functions

            ActivationFunction sigmoidFunc = (x) => 1 / (1 + Math.Exp(-x));
            ActivationFunctionDerivative sigmoidFuncDerivative = (x) => x * (1 - x);

            ActivationFunction reluFunc = (x) => Math.Max(0, x);
            ActivationFunctionDerivative reluFuncDerivative = (x) => { if (x < 0) return 0; else return 1; };

            double eluParam = 0.001;
            ActivationFunction eluFunc = (x) => { if (x > 0) return x; else return eluParam * (Math.Exp(x) - 1); };
            ActivationFunctionDerivative eluFuncDerivative = (x) => { if (x < 0) return x * eluParam * Math.Exp(x - 1); else return 1; };

            bool isEA = true;
            DataStruct[] trainingSet = new DataStruct[4];
            trainingSet[0] = new DataStruct(new double[] { 1.0, 1.0 }, new double[] { 0.0 });
            trainingSet[1] = new DataStruct(new double[] { -1.0, 1.0 }, new double[] { 1.0 });
            trainingSet[2] = new DataStruct(new double[] { 1.0, -1.0 }, new double[] { 1.0 });
            trainingSet[3] = new DataStruct(new double[] { -1.0, -1.0 }, new double[] { 0.0 });

            if (!isEA)
            {
                var network = new Network(2);
                network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
                network.AddLayer(eluFunc, eluFuncDerivative, 100);
                network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
                network.AddLayer(eluFunc, eluFuncDerivative, 100);
                network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
                network.AddLayer(eluFunc, eluFuncDerivative, 100);
                network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
                network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 1);
                network.BuildNetwork();
                network.Train(trainingSet, 0.9, 100000, 1000);

                Console.WriteLine("1 and 1");
                var res = network.Inference(trainingSet[0].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }

                Console.WriteLine("");
                Console.WriteLine("1 and 0");
                res = network.Inference(trainingSet[2].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }

                Console.WriteLine("");
                Console.WriteLine("0 and 1");
                res = network.Inference(trainingSet[1].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }

                Console.WriteLine("");
                Console.WriteLine("0 and 0");
                res = network.Inference(trainingSet[3].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }
            }
            else
            {
                LayerParams[] layers = new LayerParams[3];
                layers[0] = new LayerParams(sigmoidFunc, sigmoidFuncDerivative, 10);
                layers[1] = new LayerParams(sigmoidFunc, sigmoidFuncDerivative, 10);
                layers[2] = new LayerParams(sigmoidFunc, sigmoidFuncDerivative, 1);
                EA ea = new EA(10, 2, layers);
                ea.EvolutionaryLoop(trainingSet, 10000, 5, 0.5, 0.7, 100, 0.3);
                var winner = ea.Population.OrderBy(x => x.Fitness).ThenBy(x => x.FromGeneration).ElementAt(0);
                Console.WriteLine($"The best individual was created in generation: {winner.FromGeneration} and went through {winner.NumMutations} mutations");

                Console.WriteLine("1 and 1");
                var res = winner.Individual.Inference(trainingSet[0].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }

                Console.WriteLine("");
                Console.WriteLine("1 and 0");
                res = winner.Individual.Inference(trainingSet[2].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }

                Console.WriteLine("");
                Console.WriteLine("0 and 1");
                res = winner.Individual.Inference(trainingSet[1].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }

                Console.WriteLine("");
                Console.WriteLine("0 and 0");
                res = winner.Individual.Inference(trainingSet[3].Input);
                for (int i = 0; i < res.Length; i++)
                {
                    Console.Write($"{res[i]} ");
                }
            }
        }
    }
}
