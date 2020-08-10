using System;
using System.Collections.Generic;

namespace OOPNeuralNetworkSharp
{
    public class Network
    {
        private Layer[] Layers;
        private List<Layer> LayerList;
        public Network()
        {
        }
        public void AddLayer(Layer layer)
        {
            this.LayerList.Add(layer);
        }
        public double[] Inference(double[] input)
        {
            foreach(Layer layer in this.Layers)
            {
                input = layer.Inference(input);
            }
            return input;
        }
        public void BuildNetwork()
        {
            
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
