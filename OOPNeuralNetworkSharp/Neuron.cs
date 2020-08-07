using System;

namespace OOPNeuralNetworkSharp
{
    class Neuron
    {
        public delegate double ActivationFunction(double accumulatedSum);
        double[] weights;
        ActivationFunction activationFunction;
        double last_value;
        public Neuron(int weights, ActivationFunction activationFunction)
        {
            Random rnd= new Random();
            this.weights = new double[weights];
            for(int i = 0; i < weights; i++)
            {
                this.weights[i] = rnd.NextDouble();
            }
            this.activationFunction = activationFunction;
            this.last_value = 0;
        }

        public double Inference(double[] input)
        {
            double result = 0;
            for(int i = 0; i < input.Length; i++)
            {
                result = this.weights[i]*input[i];
            }
            result = this.activationFunction(result);
            this.last_value = result;
            return result;
        }
    }
}
