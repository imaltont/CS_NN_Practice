
namespace OOPNeuralNetworkSharp
{
    public struct LayerParams
    {
        public ActivationFunction F { get; set; }
        public ActivationFunctionDerivative G { get; set; }
        public int NumNeurons { get; set; }

        public LayerParams(ActivationFunction f, ActivationFunctionDerivative g, int n)
        {
            this.F = f;
            this.G = g;
            this.NumNeurons = n;
        }

    }
    public class Layer
    {
        Neuron[] neurons;
        public Layer(int numberOfInputs, int numberOfNeurons, ActivationFunction f, ActivationFunctionDerivative g)
        {
            this.neurons = new Neuron[numberOfNeurons];
            for (int i = 0; i < numberOfNeurons; i++)
            {
                this.neurons[i] = new Neuron(numberOfInputs, f, g);
            }
        }
        public Layer(Layer oldLayer)
        {
            this.neurons = new Neuron[oldLayer.getNeurons().Length];
            for (int i = 0; i < oldLayer.getNeurons().Length; i++)
            {
                this.neurons[i] = new Neuron(oldLayer.neurons[i]);
            }
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
