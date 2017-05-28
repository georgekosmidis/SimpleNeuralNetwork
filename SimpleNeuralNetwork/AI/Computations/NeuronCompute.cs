using SimpleNeuralNetwork.AI.Interfaces;
using SimpleNeuralNetwork.AI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.AI.Computations
{
    public class NeuronCompute : INeuronCompute
    {
        public NeuronCompute()
        {
        }

        public void SetSynapsis(Neuron neuron, List<Neuron> inputNeurons)
        {
            foreach (var inputNeuron in inputNeurons)
            {
                var synapse = new Synapse(inputNeuron, neuron);
                inputNeuron.OutputSynapses.Add(synapse);
                neuron.InputSynapses.Add(synapse);
            }
        }
    }
}
