using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public class Neuron
    {
        public List<Synapse> InputSynapses { get; private set; } = new List<Synapse>();
        public List<Synapse> OutputSynapses { get; private set; } = new List<Synapse>();

        public Double Value { get; set; }
        public Double Error { get; set; }

        public int Index { get; set; }

        public Neuron()
        {
          
        }

    }
}
