using System;

namespace OOPNeuralNetworkSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivationFunction testingFunc = (x) => 1 / (1 + Math.Exp(-x));
            ActivationFunctionDerivative testingFuncDerivative = (x) => testingFunc(x) * (1 - testingFunc(x));
            var network = new Network(2);
            network.AddLayer(testingFunc, testingFuncDerivative, 5);
            network.AddLayer(testingFunc, testingFuncDerivative, 5);
            network.AddLayer(testingFunc, testingFuncDerivative, 1);
            network.BuildNetwork();
            DataStruct[] trainingSet = new DataStruct[4];
            trainingSet[0] = new DataStruct(new double[] {1.0, 1.0}, new double[] {1.0});
            trainingSet[1] = new DataStruct(new double[] {0.0, 1.0}, new double[] {0.0});
            trainingSet[2] = new DataStruct(new double[] {1.0, 0.0}, new double[] {0.0});
            trainingSet[3] = new DataStruct(new double[] {0.0, 0.0}, new double[] {0.0});
            network.Train(trainingSet, 0.01, 100);

            Console.WriteLine("1 and 1");
            var res = network.Inference(trainingSet[0].Input);
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
