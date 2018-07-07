using SimpleNeuralNetwork.Models;
using System.Collections.Generic;

namespace SimpleNeuralNetwork.Brain.Interfaces
{
    public interface INeuronSynapsis
    {
        void Set(IMaths maths, Neuron neuron, List<Neuron> inputNeurons);
    }
}