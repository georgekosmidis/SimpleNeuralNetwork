using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Models
{
    public class Synapse : IComparable<Synapse>
    {
        public Neuron FromNeuron { get; set; }
        public Neuron ToNeuron { get; set; }
        public double Weight { get; set; } = 0;

        public int Index { get; set; }

        public Synapse(Neuron fromNeuron, Neuron toNeuron)
        {
            FromNeuron = fromNeuron;
            ToNeuron = toNeuron;
            Weight = new Random(DateTime.Now.Ticks.GetHashCode()).NextDouble();
        }

        int IComparable<Synapse>.CompareTo(Synapse s)
        {
            if (this.Index > s.Index)
                return 1;
            else if (this.Index == s.Index)
                return 0;
            else
                return -1;

        }
    }
}
