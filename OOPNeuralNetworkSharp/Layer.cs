using System;

namespace OOPNeuralNetworkSharp
{
    struct LayerParams
    {
        public ActivationFunction F { get; set; }
        public ActivationFunctionDerivative G { get; set; }
        public int NumNeurons { get; set; }

    }
    public class Layer
    {
        Neuron[] neurons;
        public Layer()
        {
        }
        public double[] Inference(double[] input)
        {
            double[] result = new double[this.neurons.Length];
            for (int i = 0; i < this.neurons.Length; i++)
            {
                result[i] = this.neurons[i].Inference(input);
            }
            return result;
        }
        public Neuron[] getNeurons()
        {
            return this.neurons;
        }
    }
}
