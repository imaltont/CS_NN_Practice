//TODO: Fix weight update so it learns more than output 50/50 every time
using System;

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
            ActivationFunctionDerivative reluFuncDerivative = (x) => {if (x < 0) return 0; else return 1;};

            double eluParam = 0.000001;
            ActivationFunction eluFunc = (x) => {if (x > 0 ) return x; else return eluParam * (Math.Exp(x) - 1);};
            ActivationFunctionDerivative eluFuncDerivative = (x) => {if (x < 0) return x*eluParam*Math.Exp(x-1); else return 1;};


            var network = new Network(2);
            network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
            network.AddLayer(eluFunc, eluFuncDerivative, 100);
            network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
            network.AddLayer(eluFunc, eluFuncDerivative, 100);
            network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 10);
            network.AddLayer(sigmoidFunc, sigmoidFuncDerivative, 1);
            network.BuildNetwork();
            DataStruct[] trainingSet = new DataStruct[4];
            trainingSet[0] = new DataStruct(new double[] {1.0, 1.0}, new double[] {0.0});
            trainingSet[1] = new DataStruct(new double[] {-1.0, 1.0}, new double[] {1.0});
            trainingSet[2] = new DataStruct(new double[] {1.0, -1.0}, new double[] {1.0});
            trainingSet[3] = new DataStruct(new double[] {-1.0, -1.0}, new double[] {0.0});
            network.Train(trainingSet, 0.5, 100000, 10000);

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
