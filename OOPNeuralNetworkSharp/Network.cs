using System;
using System.Collections.Generic;

namespace OOPNeuralNetworkSharp
{
    public class Network
    {
        List<Layer> layers;
        public Network()
        {
        }
        public double[] Inference(double[] input)
        {
            foreach(Layer layer in this.layers)
            {
                input = layer.Inference(input);
            }
            return input;
        }
        public void Train(double[] dataset, double learningRate, int epochs)
        {
            
        }
        public void Test()
        {
        }
        public void UpdateWeights()
        {
        }
    }
}
