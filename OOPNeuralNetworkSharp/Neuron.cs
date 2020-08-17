using System;

namespace OOPNeuralNetworkSharp
{
    public delegate double ActivationFunction(double accumulatedSum);
    public delegate double ActivationFunctionDerivative(double accumulatedSum);
    public class Neuron
    {
        public double[] weights;
        public ActivationFunction activationFunction;
        public ActivationFunctionDerivative activationFunctionDerivative;
        public double lastValue;
        public Neuron(int weights, ActivationFunction activationFunction, ActivationFunctionDerivative activationFunctionDerivative)
        {
            Random rnd = new Random();
            this.weights = new double[weights];
            for (int i = 0; i < weights; i++)
            {
                this.weights[i] = rnd.NextDouble() * 2 - 1;
            }
            this.activationFunction = activationFunction;
            this.activationFunctionDerivative = activationFunctionDerivative;
            this.lastValue = 0;
        }

        public double Inference(double[] input)
        {
            double result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                result += this.weights[i] * input[i];
            }
            result = this.activationFunction(result);
            this.lastValue = result;
            return result;
        }
    }
}
