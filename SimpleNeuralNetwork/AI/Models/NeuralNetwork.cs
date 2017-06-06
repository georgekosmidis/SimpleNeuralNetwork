using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Models
{
    public class NeuralNetwork
    {
        public double LearningRate = 0.3;

        public List<Neuron> InputNeurons { get; } = new List<Neuron>();
        public List<List<Neuron>> HiddenLayers { get; } = new List<List<Neuron>>();
        public List<Neuron> OutputNeurons { get; } = new List<Neuron>();

        public double NeuralNetworkError { get; set; } = 1;

        public double Divisor { get; set; }

        public MathFunctions MathFunctions { get; set; } = MathFunctions.Sigmoid;
        public string Name { get; set; } = "NueralNetwork";
    }
}
