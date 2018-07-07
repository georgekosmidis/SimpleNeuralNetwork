using System;
using System.Collections.Generic;


namespace SimpleNeuralNetwork.Models
{
    public class Neuron
    {
        public List<Synapse> InputSynapses { get; private set; } = new List<Synapse>();
        public List<Synapse> OutputSynapses { get; private set; } = new List<Synapse>();

        public Double Value { get; set; }
        public Double Error { get; set; }
        //public Double Bias { get; set; }

        public int Layer { get; set; } //0:input, last:output
        public int Index { get; set; }

    }
}
