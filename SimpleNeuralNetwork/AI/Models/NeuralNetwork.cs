using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Models
{
    public class NeuralNetwork
    {
        public double LearningRate = 0.7;
        public List<Neuron> InputNeurons { get; } = new List<Neuron>();
        public List<Neuron> HiddenNeurons { get; } = new List<Neuron>();
        public List<Neuron> OutputNeurons { get; } = new List<Neuron>();
    }
}
