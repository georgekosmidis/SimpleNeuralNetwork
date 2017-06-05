using System.Collections.Generic;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface INeuronSynapsis
    {
        void Set(IMaths maths, Neuron neuron, List<Neuron> inputNeurons);
    }
}