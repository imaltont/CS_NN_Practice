using System; 

namespace OOPNeuralNetworkSharp
{
    public delegate double ActivationFunction(double accumulatedSum);
    public delegate double ActivationFunctionDerivative(double accumulatedSum);
    public class Neuron
    {
        double[] weights;
        ActivationFunction activationFunction;
        double lastValue;
        public Neuron(int weights, ActivationFunction activationFunction)
        {
            Random rnd= new Random();
            this.weights = new double[weights];
            for(int i = 0; i < weights; i++)
            {
                this.weights[i] = rnd.NextDouble();
            }
            this.activationFunction = activationFunction;
            this.lastValue = 0;
        }

        public double Inference(double[] input)
        {
            double result = 0;
            for(int i = 0; i < input.Length; i++)
            {
                result = this.weights[i]*input[i];
            }
            result = this.activationFunction(result);
            Console.WriteLine($"{result} + tesst");
            this.lastValue = result;
            return result;
        }
    }
}
