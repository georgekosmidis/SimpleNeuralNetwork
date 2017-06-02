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
        public List<Synapse> InputSynapses { get; private set; } = new List< Synapse >();
        public List<Synapse> OutputSynapses { get; private set; } = new List<Synapse>();

        public Double Value { get; set; }
        public Double Error { get; set; }

        public int Index { get; set; }

        public Neuron()
        {
                Error = new Random(DateTime.Now.Ticks.GetHashCode()).NextDouble();
        }

        //int IComparable<Neuron>.CompareTo(Neuron n)
        //{
        //    if (this.Index > n.Index)
        //        return 1;
        //    else if (this.Index == n.Index)
        //        return 0;
        //    else
        //        return -1;

        //}
    }
}
