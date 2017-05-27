﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI
{
    public class Synapse
    {
        public Neuron FromNeuron { get; set; }
        public Neuron ToNeuron { get; set; }
        public double Weight { get; set; }

        public Synapse(Neuron fromNeuron, Neuron toNeuron)
        {            
            FromNeuron = fromNeuron;
            ToNeuron = toNeuron;
            Weight = new Random(Guid.NewGuid().GetHashCode()).NextDouble();
        }
    }
}
