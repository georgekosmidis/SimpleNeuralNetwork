using System.Collections.Generic;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface INeuronSynapsis
    {
        void Set(Neuron neuron, List<Neuron> inputNeurons);
    }
}