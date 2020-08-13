using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPNeuralNetworkSharp
{
    public struct DataStruct
    {
        public double[] Input { get; set; }
        public double[] Output { get; set; }
        public DataStruct(double[] input, double[] output)
        {
            this.Input = input;
            this.Output = output;
        }
    }
    public class Network
    {
        private Layer[] Layers;
        private List<LayerParams> LayerList;
        private int NumberOfInputs { get; }
        public Network(int numberOfInputs)
        {
            this.NumberOfInputs = numberOfInputs;
            this.LayerList = new List<LayerParams>();
        }
        public void AddLayer(ActivationFunction f, ActivationFunctionDerivative g, int numberOfNeurons)
        {
            this.LayerList.Add(new LayerParams(f, g, numberOfNeurons));
        }
        public double[] Inference(double[] input)
        {
            foreach (Layer layer in this.Layers)
            {
                input = layer.Inference(input);
            }
            return input;
        }
        public void BuildNetwork()
        {
            this.Layers = new Layer[this.LayerList.Count];
            for (int i = 0; i < this.LayerList.Count; i++)
            {
                if (i == 0)
                {
                    this.Layers[i] = new Layer(this.NumberOfInputs, LayerList[i].NumNeurons, LayerList[i].F, LayerList[i].G);
                }
                else
                {
                    this.Layers[i] = new Layer(LayerList[i - 1].NumNeurons, LayerList[i].NumNeurons, LayerList[i].F, LayerList[i].G);
                }
            }
        }
        //no batch training 
        public void Train(DataStruct[] dataset, double learningRate, int epochs)
        {
            for (int i = 0; i < epochs; i++)
            {
                double accumulatedError = 0;
                //this.Test(dataset);
                foreach (var example in dataset)
                {
                    double[] result = this.Inference(example.Input);
                    double[] delta = this.Compare(result, example.Output);

                    this.Backpropagation(result, delta);
                    //error is just for printing results for now.
                    double[] error = new double[delta.Length];
                    for (int j = 0; j < delta.Length; j++)
                    {
                        error[j] = Math.Pow(delta[j], 2);
                    }
                    accumulatedError +=  error.Sum() / error.Length;
                    //TODO: Update weights/backpropagation.
                }
                Console.WriteLine($"Average error rate: {accumulatedError / dataset.Length}");
            }
        }

        private double[] Compare(double[] result, double[] output)
        {
            double[] local_result = new double[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                local_result[i] = output[i] - result[i];
            }
            return local_result;
        }

        public void Test(DataStruct[] dataset)
        {
        }
        private void Backpropagation(double[] results, double[] delta)
        {
            //TODO: put this into Compare as well in some way, to avoid the extra for loop
            for (int i = 0; i < delta.Length; i++)
            {
                delta[i] *= this.Layers[this.Layers.Length-1].getNeurons()[i].activationFunctionDerivative(results[i]);
            }
        }
        public void UpdateWeights(double learningRate, double[] error)
        {
        }
    }
}
