using System;

namespace OOPNeuralNetworkSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            ActivationFunction testingFunc = (x) => x;
            ActivationFunctionDerivative testingFuncDerivative = (x) => x;
            var network = new Network(5);
            network.AddLayer(testingFunc, testingFuncDerivative, 5);
            network.BuildNetwork();
            double[] inp =  new double[]{1.0, 1.0, 1.0, 1.0, 1.0};
            double[] output = network.Inference(inp);
            foreach (var item in output)
            {
                Console.WriteLine($"{item}");
            }
        }
    }
}
