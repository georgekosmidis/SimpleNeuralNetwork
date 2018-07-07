using SimpleNeuralNetwork.Brain.Interfaces;
using SimpleNeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork.Brain.Computations
{
    public class NeuronSynapsis : INeuronSynapsis
    {

        public NeuronSynapsis()
        {
        }

        public void Set(IMaths maths, Neuron neuron, List<Neuron> inputNeurons)
        {
            foreach (var inputNeuron in inputNeurons)
            {
                var synapse = new Synapse() {
                    FromNeuron = inputNeuron,
                    ToNeuron = neuron,
                    Weight = maths.Random()
                };
                inputNeuron.OutputSynapses.Add(synapse);
                neuron.InputSynapses.Add(synapse);
            }
        }
    }
}
