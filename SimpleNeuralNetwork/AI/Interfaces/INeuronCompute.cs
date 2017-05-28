using System.Collections.Generic;

namespace SimpleNeuralNetwork.AI.Interfaces
{
    public interface INeuronCompute
    {
        void SetSynapsis(Neuron neuron, List<Neuron> inputNeurons);
    }
}