//TODO: Fix weight update so it learns more than output 50/50 every time
using System;

namespace OOPNeuralNetworkSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivationFunction testingFunc = (x) => 1 / (1 + Math.Exp(-x));
            ActivationFunctionDerivative testingFuncDerivative = (x) => x * (1 - x);
//            ActivationFunction testingFunc = (x) => (Math.Exp(2*x) - 1) / (Math.Exp(2 * x) + 1);
//            ActivationFunctionDerivative testingFuncDerivative = (x) => 1 - Math.Pow(x, 2);
            var network = new Network(2);
            network.AddLayer(testingFunc, testingFuncDerivative, 10);
            network.AddLayer(testingFunc, testingFuncDerivative, 10);
            network.AddLayer(testingFunc, testingFuncDerivative, 10);
            network.AddLayer(testingFunc, testingFuncDerivative, 10);
            network.AddLayer(testingFunc, testingFuncDerivative, 2);
            network.BuildNetwork();
            DataStruct[] trainingSet = new DataStruct[4];
            trainingSet[0] = new DataStruct(new double[] {1.0, 1.0}, new double[] {0.0, 1.0});
            trainingSet[1] = new DataStruct(new double[] {-1.0, 1.0}, new double[] {1.0, 0.0});
            trainingSet[2] = new DataStruct(new double[] {1.0, -1.0}, new double[] {1.0, 0.0});
            trainingSet[3] = new DataStruct(new double[] {-1.0, -1.0}, new double[] {0.0, 1.0});
            network.Train(trainingSet, 0.1, 100000);

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
//            double[] output = network.Inference(inp);
            
        }
    }
}
