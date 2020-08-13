using System;

namespace OOPNeuralNetworkSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ActivationFunction testingFunc = (x) => x;
            ActivationFunctionDerivative testingFuncDerivative = (x) => 1;
            var network = new Network(2);
            network.AddLayer(testingFunc, testingFuncDerivative, 2);
            network.BuildNetwork();
            DataStruct[] trainingSet = new DataStruct[1];
            trainingSet[0] = new DataStruct(new double[] {1.0, 1.0}, new double[] {1.0,0.0});
            network.Train(trainingSet, 0.1, 10);
//            double[] output = network.Inference(inp);
            
        }
    }
}
