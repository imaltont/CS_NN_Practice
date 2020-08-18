
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
        private List<LayerParams> LayerList;
        public Layer[] Layers;
        public int NumberOfInputs { get; }
        public Network(int numberOfInputs)
        {
            this.NumberOfInputs = numberOfInputs;
            this.LayerList = new List<LayerParams>();
        }
        public Network(Network oldNetwork)
        {
            this.NumberOfInputs = oldNetwork.NumberOfInputs;
            this.Layers = new Layer[oldNetwork.Layers.Length];
            for (int i = 0; i < this.Layers.Length; i++)
            {
                this.Layers[i] = new Layer(oldNetwork.Layers[i]);
            }
        
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
        public void Train(DataStruct[] dataset, double learningRate, int epochs, int updateInterval)
        {
            for (int i = 0; i < epochs; i++)
            {
                double accumulatedError = 0;
                if (i+1 % updateInterval == 0)
                {
                    learningRate = learningRate - (learningRate / (epochs));
                }
                foreach (var example in dataset)
                {
                    double[] result = this.Inference(example.Input);
                    double[] delta = this.Compare(example.Output, result);

                    double[] MSE = new double[delta.Length];
                    for (int iDelta = 0; iDelta < delta.Length; iDelta++)
                    {
                        MSE[iDelta] = Math.Pow(delta[iDelta], 2);
                    }
                    this.Backpropagation(result, delta, learningRate, example.Input);
                    accumulatedError += MSE.Sum() / MSE.Length;
                }
                Console.WriteLine($"Average error rate: {accumulatedError / dataset.Length}");
                if (accumulatedError == 0)
                    break;
            }
        }

        public double[] Compare(double[] expected, double[] output)
        {
            double[] local_result = new double[expected.Length];
            for (int i = 0; i < expected.Length; i++)
            {
                local_result[i] = output[i] - expected[i];
            }
            return local_result;
        }

        private void Backpropagation(double[] results, double[] delta, double learningrate, double[] inputs)
        {
            List<double> prevDelta = delta.ToList();
            for (int i = this.Layers.Length - 1; i >= 0; i--)
            {
                List<double> currentDelta = new List<double>();
                for (int w = 0; w < this.Layers[i].getNeurons()[0].weights.Length; w++)
                {
                    currentDelta.Add(0);
                }
                for (int j = 0; j < this.Layers[i].getNeurons().Length; j++)
                {
                    double localDelta = prevDelta[j] * this.Layers[i].getNeurons()[j].activationFunctionDerivative(this.Layers[i].getNeurons()[j].lastValue);
                    for (int w = 0; w < this.Layers[i].getNeurons()[j].weights.Length; w++)
                    {
                        currentDelta[w] += localDelta * this.Layers[i].getNeurons()[j].weights[w];
                        if (i > 0)
                        {
                            this.Layers[i].getNeurons()[j].weights[w] -= learningrate * this.Layers[i - 1].getNeurons()[w].lastValue * localDelta;
                        }
                        else
                        {
                            this.Layers[i].getNeurons()[j].weights[w] -= learningrate * inputs[w] * localDelta;
                        }
                    }

                }
                prevDelta = new List<double>(currentDelta);
            }
        }
    }
}
