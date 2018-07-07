using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Models
{
    public class NeuralNetwork
    {
        public double LearningRate = 0.01;

        public List<Neuron> InputNeurons { get; } = new List<Neuron>();
        public List<List<Neuron>> HiddenLayers { get; } = new List<List<Neuron>>();
        public List<Neuron> OutputNeurons { get; } = new List<Neuron>();

        public double NeuralNetworkError { get; set; } = 1;

        public double Divisor { get; set; }

        public eMathFunctions MathFunctions { get; set; } = eMathFunctions.Sigmoid;
        public string Name { get; set; } = "NueralNetwork";
    }
}
